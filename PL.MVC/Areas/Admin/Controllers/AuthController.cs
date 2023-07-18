using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models.Auth;
using PL.Services.Interfaces.UserService;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using PL.Services.Admin;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
public class AuthController : BaseMvcController
{
	public AuthController(
		IUserService userService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
	}

	[AllowAnonymous]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		var user = await _userService.GetUserByEmailAsync(model.Login);

		if (user == null)
		{
			ModelState.AddModelError(string.Empty, "Invalid username or password");
			return View(model);
		}

		if (!await _userService.CheckPasswordAsync(model.Login, model.Password))
		{
			ModelState.AddModelError(string.Empty, "Invalid username or password");
			return View(model);
		}

		if (!user.Roles.HasFlag(UserRole.Admin)
		    && !user.Roles.HasFlag(UserRole.Operator)
		    && !user.Roles.HasFlag(UserRole.Driver)
		    && !user.Roles.HasFlag(UserRole.Dispatcher))
		{
			ModelState.AddModelError(string.Empty, "Not an Admin!");
			return View(model);
		}
        
		var claims = _userService.GetUserClaims(user);

		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		var authProperties = new AuthenticationProperties
		{
			IsPersistent = model.RememberMe
		};

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);

		return RedirectToAction("Index", "Home");
	}
	
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

		return RedirectToAction("Index", "Home");
	}
}
