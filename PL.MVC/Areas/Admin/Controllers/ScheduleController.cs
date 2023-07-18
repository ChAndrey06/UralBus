using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PL.Entities.Trip;
using PL.Entities.TripRoute;
using PL.MVC.Areas.Admin.Models;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Client;
using PL.Services.Interfaces.UserService;
using System;
using PL.Services.Client.Models;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = nameof(UserRole.Admin) + ", " + nameof(UserRole.Dispatcher))]
public class ScheduleController : BaseMvcController
{
	private readonly TripRoutesService _tripRoutesService;
	private readonly PL.Services.Client.TripsService _tripsService;
    private readonly PL.Services.Admin.TripsService _tripsServiceAdmin;
    private readonly PL.Services.Admin.TransportsService _transportsService;
    private readonly DriversService _driversService;
    private readonly KontragentsService _kontragentsService;
    public ScheduleController(
		IUserService userService,
		TripRoutesService tripRoutesService,
		TransportsService transportsService,
		DriversService driversService,
		KontragentsService kontragentsService,
		PL.Services.Client.TripsService tripsService,
        PL.Services.Admin.TripsService tripsServiceAdmin,
        CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_tripRoutesService = tripRoutesService;
		_tripsService = tripsService;
        _tripsServiceAdmin = tripsServiceAdmin;
        _transportsService = transportsService;
		_driversService = driversService;
		_kontragentsService = kontragentsService;
	}

	public async Task<IActionResult> Index(ScheduleModel scheduleModel)
	{
		var model = new ScheduleModel();
		var filterModel = new TripsFilterModel();

		model.StartDate = scheduleModel.StartDate;
		model.EndDate = scheduleModel.EndDate;

		if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
		{
			model.EndDate = model.StartDate.AddDays(1);
		}

		if ((int)(model.EndDate - model.StartDate).TotalDays == 0)
		{
			model.EndDate = model.StartDate.AddDays(1);
		}

		var triproutes = new SearchResultViewModel<TripRoute, FilterModel>(
			await _tripRoutesService.GetAsync(),
			filterModel
		).Objects;

		filterModel.DepartureTime = model.StartDate;
		filterModel.EndTime = model.EndDate; 
		
		if (scheduleModel.ChooseTripRouteId != Guid.Empty)
        {
            filterModel.TripRouteId = scheduleModel.ChooseTripRouteId;
        }

        var trips = new SearchResultViewModel<Trip, FilterModel>(
			await _tripsServiceAdmin.GetTripsSchedule(filterModel),
			filterModel
		).Objects;

		var tempTrips = trips.Items;
		
		model.Trips = tempTrips.ToList();

		model.TripRoutes = triproutes.Items.ToList();

		model.ChooseTripRouteId = scheduleModel.ChooseTripRouteId;



		var datesBetween = Enumerable
			.Range(0, (int)(model.EndDate - model.StartDate).TotalDays + 1)
			.Select(diff => new IndexInfoDay()
			{
				Date = model.StartDate.AddDays(diff),
				Trips = model.Trips.Where(r => r.DepartureTime.Year == model.StartDate.AddDays(diff).Year &&
												r.DepartureTime.Month == model.StartDate.AddDays(diff).Month &&
												r.DepartureTime.Day == model.StartDate.AddDays(diff).Day

										).OrderBy(t => t.DepartureTime).ToList()
			})
			.ToArray();

		model.datesBetween = datesBetween;

		return View(model);
	}

	public async Task<IActionResult> Update(ScheduleModel scheduleModel)
	{
		var model = new ScheduleModel();
		var filterModel = new FilterModel();

        var triproutes = new SearchResultViewModel<TripRoute, FilterModel>(
			await _tripRoutesService.GetAsync(),
			filterModel
		).Objects;

        var tripsFilter = new TripsFilterModel()
		{
            DepartureTime = scheduleModel.StartDate
		};
        if (scheduleModel.ChooseTripRouteId != Guid.Empty)
        {
            tripsFilter.TripRouteId = scheduleModel.ChooseTripRouteId;
        }

        var trips = new SearchResultViewModel<Trip, FilterModel>(
			await _tripsService.GetTripsCalendar(tripsFilter),
            tripsFilter
        ).Objects;

		model.TripRoutes = triproutes.Items.ToList();

		var tempTrips = trips.Items;
		
		model.Trips = tempTrips.ToList();

		foreach (var item in model.Trips)
		{
			var countSeats = (await _transportsService.GetByIdAsync((Guid)item.TransportId)).Seats;
			model.Trips.First(r => r.Id == item.Id).SeatsCount = countSeats;

		}

		model.StartDate = scheduleModel.StartDate;
		model.ChooseTripRouteId = scheduleModel.ChooseTripRouteId;

        return View(model);
	}


	[HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> UpdateCalendar([FromBody]List<CalendarUpdateItemDto> dtos)
    {
	    foreach (var item in dtos)
	    {
		    //var id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;

			if (item.Id == Guid.Empty)
			{
				var id = Guid.NewGuid();

                var saveModel = new Trip()
                {
                    Id = id,
                    DepartureTime = item.Start,
					TripRouteId = item.TripRouteId
                };

                await _tripsService.AddAsync(saveModel);
            }
			else
			{
				var id = item.Id;

				//@TODO: запросы в цикле убрать
                 var model = await _tripsService.GetByIdAsync(id);

                var saveModel = new Trip()
                {
                    Id = id,
                    DepartureTime = item.Start,
                    Price = model.Price,
					TripRouteId = model.TripRouteId,
					TransportId = model.TransportId,
					DriverId = model.DriverId,
					CarrierId = model.CarrierId
				};

                //@TODO: запросы в цикле убрать
                await _tripsService.UpdateAsync(saveModel);
            }

	    }

	    return Ok();
    }

    public class CalendarUpdateItemDto 
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Guid Id { get; set; }
    public Guid TripRouteId { get; set; }
    public ExtendedPropsDto ExtendedProps { get; set; }
}

public class ExtendedPropsDto
{
    public string Driver { get; set; }
    public int SeatsCount { get; set; }
    public int SeatsBusy { get; set; }
}


}