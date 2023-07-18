using Common.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Admin;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Controllers.Auth;

[Authorize(Roles = nameof(UserRole.Client))]
public class UserController : BaseMvcController
{
	public UserController(
        IUserService userService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
    }
	
	public async Task<IActionResult> Delete()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		await _userService.DeleteUserByIdAsync(GetCurrentUser().Id);
		
		return RedirectToAction("Index", "Home");
	}
}