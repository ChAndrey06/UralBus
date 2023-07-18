using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Admin.Models;
using PL.Services.Admin;
using PL.Services.Interfaces.UserService;
using PL.MVC.Models;
using PL.Entities.Trip;
using System.Text.Json;
using System;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using PL.Entities.File;
using PL.Entities.TripRoute;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin) + ", " + nameof(UserRole.Driver) + ", " + nameof(UserRole.Dispatcher))]
public class TripRoutesController : BaseMvcController
{
	private readonly TripRoutesService _tripRoutesService;
	private readonly RoutePointsService _routePointsService;

	public TripRoutesController(
		IUserService userService,
		TripRoutesService tripRoutesService,
		RoutePointsService routePointsService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_tripRoutesService = tripRoutesService;
		_routePointsService = routePointsService;
	}

	[HttpGet]
	public async Task<IActionResult> Index(FilterModel filterModel)
	{
		var model = new SearchResultViewModel<TripRoute, FilterModel>(
			await _tripRoutesService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Update(Guid? id)
	{
		TripRoute trip = null;
		if(id.HasValue)
			trip = await _tripRoutesService.GetByIdAsync((Guid) id);
		var routePoints = await _routePointsService.GetAsync();
		
		ViewBag.AllRoutePoints = routePoints.Items.ToList();

		return View(trip);
	}

	[HttpPost]
	public async Task<IActionResult> Update(TripRoute trip)
	{
		if (trip.PopularDestination && trip.PopularDestinationPicturePath is not null)
		{
			trip.PopularDestinationPicture = new S3File()
			{
				Path = trip.PopularDestinationPicturePath,
				UploaderId = GetCurrentUser().Id,
				OriginalName = string.Empty,
				DocumentType = DocumentType.PopularDestinations,
				ContentType = string.Empty
			};
		}
		
		if (trip.Id == Guid.Empty)
			await _tripRoutesService.AddAsync(trip);
		else
			await _tripRoutesService.UpdateAsync(trip);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _tripRoutesService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
