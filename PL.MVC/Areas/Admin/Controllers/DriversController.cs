using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;
using PL.Entities.PersonIdentity;
using PL.MVC.Models;
using PL.Services.Admin;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class DriversController : BaseMvcController
{
	private readonly DriversService _driversService;
	private readonly UsersService _usersService;
	private readonly KontragentsService _kontragentsService;

	public DriversController(
		IUserService userService,
		DriversService driversService,
		UsersService usersService,
		KontragentsService kontragentsService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_driversService = driversService;
		_usersService = usersService;
		_kontragentsService = kontragentsService;
	}

	public async Task<IActionResult> Index(FilterModel filterModel)
	{
		var model = new SearchResultViewModel<DriverPersonIdentity, FilterModel>(
			await _driversService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	public async Task<IActionResult> Update(Guid? id)
	{
		DriverPersonIdentity? model = new DriverPersonIdentity();

		if (id is not null)
		{
			model = await _driversService.GetByIdAsync((Guid)id);

			if (model is null)
			{
				return NotFound();
			}
		}

		ViewBag.AllUsers = await _usersService.GetAsync();
		ViewBag.AllCarriers = await _kontragentsService.GetAsync(KontragentIdentityType.Carrier.ToString());
		
		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Update(DriverPersonIdentity model)
	{
		var oldDriver = await _driversService.GetByIdAsync(model.Id);
		model.IdentityType = PersonIdentityType.Driver;

		if (oldDriver is null)
			await _driversService.AddAsync(model);
		else
			await _driversService.UpdateAsync(model);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _driversService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
