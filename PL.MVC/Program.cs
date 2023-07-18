using System.Text.Json.Serialization;
using Common.Enums;
using DependencyInjections;
using Microsoft.AspNetCore.Authentication.Cookies;
using PL.MVC.Areas.Admin.FileManager;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.Configure<FileManagerOptions>(builder.Configuration.GetSection("FileManager"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/";
	});

//Register services
var resolver = builder.Configuration.GetDependencyResolver();
resolver.RegisterServices(builder.Services);

//builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//	endpoints.MapAreaControllerRoute(
//		name: "Admin",
//		areaName: "Admin",
//		pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
//	);
//	endpoints.MapControllerRoute(
//		name: "default",
//		pattern: "{controller=Home}/{action=Index}/{id?}");
//});

app.MapAreaControllerRoute(
	name: "Admin",
	areaName: "Admin",
	pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
	if (context.Request.Path.Value.Contains("/Admin"))
	{
		var user = context.User;
		if (user.Identity != null 
				&& user.Identity.IsAuthenticated 
				&& (user.IsInRole(UserRole.Admin.ToString())
				|| user.IsInRole(UserRole.Operator.ToString())
				|| user.IsInRole(UserRole.Dispatcher.ToString())
				|| user.IsInRole(UserRole.Driver.ToString())))
		{
			await next.Invoke(); // Allow access to route
		}
		else if (context.Request.Path.Value.Contains("/Admin/Auth/Login"))
		{
			if(!user?.Identity?.IsAuthenticated??false)
			{
				await next.Invoke();
			}
			else
			{
				//@TODO: Редирект на Access Denied
				context.Response.StatusCode = 403;
				await context.Response.WriteAsync("Access Denied. Admin role required.");
			}
		}
		else
		{
			context.Response.Redirect("/Admin/Auth/Login");
		}
	}
	else
	{
		await next.Invoke(); // Allow access to all other routes
	}
});

app.Run();