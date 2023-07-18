using Common.Enums;
using PL.Entities.User;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Middlewares;

public class ProfileControllerRouterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ProfileControllerRouterMiddleware> _logger;

    public ProfileControllerRouterMiddleware(RequestDelegate next, ILogger<ProfileControllerRouterMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        string path = context.Request.Path.Value.ToLower();

        if (path.StartsWith("/profile"))
        {
            var user = await userService.GetUserByEmailAsync(context?.User?.Identity?.Name??"");

            if (user == null)
            {
                context.Response.Redirect("/account/login");
                return;
            }

            var profileRoutes = new Dictionary<UserRole, string>()
            {
                { UserRole.Client, "/clientprofile/index" },
                { UserRole.Driver, "/driverprofile/index" },
                { UserRole.Dispatcher, "/dispatcherprofile/index" },
                { UserRole.Operator, "/operatorprofile/index" },
                { UserRole.None, "/profile/index" }
            };

            context.Request.Path = profileRoutes.ContainsKey(user.Roles) ? profileRoutes[user.Roles] : "/profile/index";

        }

        await _next(context);
    }
}
