using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PL.MVC.Controllers.Base;
using PL.Services.Admin;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = nameof(UserRole.Admin) 
                   + ", " + nameof(UserRole.Dispatcher)
				   + ", " + nameof(UserRole.Operator) 
                   + ", " + nameof(UserRole.Driver))]
public class HomeController : BaseMvcController
{
    private readonly ILogger<HomeController> _logger;

	public HomeController(
		IUserService userService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
	}

	[AllowAnonymous]
	public IActionResult Index()
    {
	    if (!(User?.IsInRole(UserRole.Admin.ToString()) ?? false)
	        && !(User?.IsInRole(UserRole.Operator.ToString()) ?? false)
	        && !(User?.IsInRole(UserRole.Driver.ToString()) ?? false)
	        && !(User?.IsInRole(UserRole.Dispatcher.ToString()) ?? false))
	    {
		    return RedirectToAction("Login", "Auth");
	    }
        return View();
    }
}