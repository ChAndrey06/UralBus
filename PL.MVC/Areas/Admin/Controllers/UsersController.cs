using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Interfaces.UserService;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using PL.Services.Admin.Models;
using PL.Services.Admin;
using PL.MVC.Models;
using PL.Entities.User;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class UsersController : BaseMvcController
{
	private readonly UsersService _usersService;

	public UsersController(
		IUserService userService,
		UsersService usersService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_usersService = usersService;
	}

	[HttpGet]
	public async Task<IActionResult> Index(UsersFilterModel filterModel)
	{
		var model = new SearchResultViewModel<User, UsersFilterModel>(
			await _usersService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}
	
	public async Task<IActionResult> Update(Guid id)
	{
		User? user = null;

		if (id != Guid.Empty)
		{
			user = await _usersService.GetByIdAsync(id);

			if (user is null)
				return NotFound();
		}

		return View(user);
	}

	[HttpPost]
	public async Task<IActionResult> Update(User user, string newPassword)
	{
		if (user.Id == Guid.Empty)
			await _usersService.AddAsync(user);
		else
		{
			if (!string.IsNullOrEmpty(newPassword))
				user.Password = newPassword;
			await _usersService.UpdateAsync(user);
		}

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _usersService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
