using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Interfaces.UserService;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Entities.Trip;
using PL.Services.Admin.Models;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class TripsController : BaseMvcController
{
	private readonly TripsService _tripsService;
	private readonly TripRoutesService _tripRoutesService;
	private readonly TransportsService _transportsService;
	private readonly DriversService _driversService;
	private readonly KontragentsService _kontragentsService;

	public TripsController(
		IUserService userService,
		TripsService tripsService,
		TripRoutesService tripRoutesService,
		TransportsService transportsService,
		DriversService driversService,
		KontragentsService kontragentsService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_tripsService = tripsService;
        _tripRoutesService = tripRoutesService;
        _transportsService = transportsService;
        _driversService = driversService;
        _kontragentsService = kontragentsService;
    }

	public async Task<IActionResult> Index(FilterModel filterModel)
	{
		var model = new SearchResultViewModel<Trip, FilterModel>(
			await _tripsService.GetAsync(filterModel),
			filterModel
		);
		
		return View(model);
	}

	public async Task<IActionResult> Update(Guid? id)
	{
		Trip? model = new Trip();

		if (id is not null)
		{
			model = await _tripsService.GetByIdAsync((Guid)id);

			if (model is null)
			{
				return NotFound();
			}
		}
		
		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Update(Trip model)
	{
		var oldTransport = await _tripsService.GetByIdAsync(model.Id);

		if (oldTransport is null)
			await _tripsService.AddAsync(model);
		else
			await _tripsService.UpdateAsync(model);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _tripsService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
