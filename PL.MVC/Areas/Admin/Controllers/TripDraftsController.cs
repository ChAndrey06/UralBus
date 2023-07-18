using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.TripDraft;
using PL.MVC.Areas.Admin.Models;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;
using System.Globalization;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin) + ", " + nameof(UserRole.Operator))]
public class TripDraftsController:BaseMvcController
{
    private readonly TripDraftsService _tripDraftsService;
    private readonly TripRoutesService _tripRoutesService;
    
    public TripDraftsController(IUserService userService, CommonConfigsService commonConfigsService, TripDraftsService tripDraftsService, TripRoutesService tripRoutesService) : base(userService, commonConfigsService)
    {
        _tripDraftsService = tripDraftsService;
        _tripRoutesService = tripRoutesService;
    }
    public async Task<IActionResult> Index(TripDraftsFilterModel filterModel)
    {
        var model = new SearchResultViewModel<TripDraft, TripDraftsFilterModel>(
            await _tripDraftsService.GetAsync(filterModel),
            filterModel
        );
		
        return View(model);
    }
    
    public async Task<IActionResult> Calendar(TripDraftsFilterModel filterModel)
    {
        var tripDrafts = await _tripDraftsService.GetAsync(filterModel);
        var tripRoutes = await _tripRoutesService.GetAsync(filterModel);
        
        var model = new CalendarModel
        {
            TripDrafts = tripDrafts,
            TripRoutes = tripRoutes,
            Filter = filterModel
        };


        return View(model);
    }

    public async Task<IActionResult> Update(Guid? id)
    {
        TripDraft? model = new ();

        if (id is not null)
        {
            model = await _tripDraftsService.GetByIdAsync((Guid)id);

			if (model is null)
            {
                return NotFound();
            }

			var tripRoute = await _tripRoutesService.GetByIdAsync(model.TripRouteId);
			var minutes = tripRoute.TripRoutePoints.Sum(x => x.MinutesFromStart);

			model.EndTimeOfDay =
			   DateTime.ParseExact(model.StartTimeOfDay, "HH:mm:ss",
									   CultureInfo.InvariantCulture).AddMinutes(minutes).ToString("HH:mm:ss");
			model.EndDayOfWeek = model.StartDayOfWeek;
			if (minutes >= 1440
				||
				DateTime.ParseExact(model.StartTimeOfDay, "HH:mm:ss", CultureInfo.InvariantCulture)
				>
				DateTime.ParseExact(model.EndTimeOfDay, "HH:mm:ss", CultureInfo.InvariantCulture))
			{
				var additionalDays = Math.Floor(minutes / 1440.0);

				var needDay = (int)model.StartDayOfWeek + 1 + additionalDays;
				while (needDay > 6)
				{
					needDay = needDay - 6;
				}
				model.EndDayOfWeek = (Common.Enums.DayOfWeek)(needDay);
			}
		}
		
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TripDraft model)
    {
        var oldTransport = await _tripDraftsService.GetByIdAsync(model.Id);

        if (oldTransport is null)
            await _tripDraftsService.AddAsync(model);

		var tripRoute = await _tripRoutesService.GetByIdAsync(model.TripRouteId);
		var minutes = tripRoute.TripRoutePoints.Sum(x => x.MinutesFromStart);

		model.EndTimeOfDay =
		   DateTime.ParseExact(model.StartTimeOfDay, "HH:mm:ss",
								   CultureInfo.InvariantCulture).AddMinutes(minutes).ToString("HH:mm:ss");
		model.EndDayOfWeek = model.StartDayOfWeek;
		if (minutes >= 1440
			||
			DateTime.ParseExact(model.StartTimeOfDay, "HH:mm:ss", CultureInfo.InvariantCulture)
			>
			DateTime.ParseExact(model.EndTimeOfDay, "HH:mm:ss", CultureInfo.InvariantCulture))
		{
			var additionalDays = Math.Floor(minutes / 1440.0);

			var needDay = (int)model.StartDayOfWeek + 1 + additionalDays;
			while (needDay > 6)
			{
				needDay = needDay - 6;
			}
			model.EndDayOfWeek = (Common.Enums.DayOfWeek)(needDay);
		}

		await _tripDraftsService.UpdateAsync(model);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _tripDraftsService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
    
}