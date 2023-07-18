using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.AuthCode;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class AuthCodesController : BaseMvcController
{
    private readonly AuthCodesService _authCodesService;
	private readonly UsersService _usersService;

	public AuthCodesController(
        IUserService userService, 
        AuthCodesService authCodesService,
		UsersService usersService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
    {
        _authCodesService = authCodesService;
		_usersService = usersService;
	}

    public IActionResult Index()
    {
        return View();
    }

	[HttpGet]
	public async Task<IActionResult> Index(FilterModel filterModel)
	{
		var model = new SearchResultViewModel<AuthCode, FilterModel>(
			await _authCodesService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Update(Guid? id)
	{
		AuthCode authCode = null;

		if (id.HasValue)
			authCode = await _authCodesService.GetByIdAsync((Guid)id);

		var users = await _usersService.GetAsync();

		ViewBag.AllUsers = users.Items.ToList();

		return View(authCode);
	}

	[HttpPost]
	public async Task<IActionResult> Update(AuthCode code)
	{
		if (code.Id == Guid.Empty)
			await _authCodesService.AddAsync(code);
		else
			await _authCodesService.UpdateAsync(code);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _authCodesService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
