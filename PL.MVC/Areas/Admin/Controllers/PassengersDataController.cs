using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.PassengerData;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class PassengersDataController : BaseMvcController
{
	private readonly PassengersDataService _passengersDataService;

	public PassengersDataController(
		IUserService userService,
		PassengersDataService passengersDataService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_passengersDataService = passengersDataService;
	}

    public async Task<IActionResult> Index(FilterModel filterModel)
    {
		var model = new SearchResultViewModel<PassengerData, FilterModel>(
			await _passengersDataService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	public async Task<IActionResult> Update(Guid? id)
	{
		PassengerData? model = new PassengerData();

		if (id is not null)
		{
			model = await _passengersDataService.GetByIdAsync((Guid)id);

			if (model is null)
			{
				return NotFound();
			}
		}

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Update(PassengerData model)
	{
		var oldPassengerData = await _passengersDataService.GetByIdAsync(model.Id);

		if (oldPassengerData is null)
			await _passengersDataService.AddAsync(model);
		else
			await _passengersDataService.UpdateAsync(model);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _passengersDataService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}