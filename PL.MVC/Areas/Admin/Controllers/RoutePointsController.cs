using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.RoutePoint;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;
using PL.Services.Admin;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class RoutePointsController : BaseMvcController
{
    private readonly RoutePointsService _routePointsService;

    public RoutePointsController(
        IUserService userService,
        RoutePointsService routePointsService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
        _routePointsService = routePointsService;
	}

	[HttpGet]
	public async Task<IActionResult> Index(FilterModel filterModel)
	{
		var model = new SearchResultViewModel<RoutePoint, FilterModel>(
			await _routePointsService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	public async Task<IActionResult> Update(Guid? id)
	{
		RoutePoint? model = new RoutePoint();

		if (id is not null)
		{
			model = await _routePointsService.GetByIdAsync((Guid)id);

			if (model is null)
			{
				return NotFound();
			}
		}

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Update(RoutePoint model)
	{
		var oldRoutePoint = await _routePointsService.GetByIdAsync(model.Id);

		if (oldRoutePoint is null)
			await _routePointsService.AddAsync(model);
		else
			await _routePointsService.UpdateAsync(model);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _routePointsService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
