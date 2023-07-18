using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.Order;
using PL.Entities.Trip;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Client;
using PL.Services.Client.Models;
using PL.Services.Interfaces.UserService;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;


namespace PL.MVC.Controllers.Trips;

[Authorize(Roles = nameof(UserRole.Client))]
public class TripsController : BaseMvcController
{
    private readonly TripsService _tripsService;
    private readonly RoutePointsService _routePointsService;
    private readonly PL.Services.Admin.TripRoutePointsService _tripRoutePointsService;

    public TripsController(
        IUserService userService,
        TripsService tripsService,
        RoutePointsService routePointsService,
        PL.Services.Admin.CommonConfigsService commonConfigsService,
        PL.Services.Admin.TripRoutePointsService tripRoutePointsService
    ) : base(userService, commonConfigsService)
    {
        _tripsService = tripsService;
        _routePointsService = routePointsService;
        _tripRoutePointsService = tripRoutePointsService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index(TripsFilterModel filterModel)
    {
        var model = new SearchResultViewModel<Trip, TripsFilterModel>(
            await _tripsService.GetAsync(filterModel),
            filterModel
        );

        var fromPoint = await _routePointsService.GetByIdAsync(filterModel.FromRoutePointId);
        var toPoint = await _routePointsService.GetByIdAsync(filterModel.ToRoutePointId);

        ViewBag.FilterModel = new
        {
            From = new
            {
                fromPoint?.Id,
                fromPoint?.Title
            },
            To = new
            {
                toPoint?.Id,
                toPoint?.Title
            },
            Departure = filterModel?.DepartureTime
        };

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateList([FromBody] TripsFilterModel filterModel)
    {
        var model = new SearchResultViewModel<Trip, TripsFilterModel>(
            await _tripsService.GetAsync(filterModel),
            filterModel
        );

        return Ok(model);
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        var trip = await _tripsService.GetByIdAsync(id);

        return View(trip);
    }

    [HttpGet]
    public async Task<IEnumerable<object>> Points(Guid id)
    {
        return await _tripsService.GetPointsByTripIdAsync(id);
    }

    [HttpGet]
    public async Task<IActionResult> Passsengers([FromQuery] CreateOrderViewModel model)
    {
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Confirm(CreateOrderViewModel model)
    {
        var order = new Order
        {
            AdultsCount = model.AdultsCount,
            ChildrenCount = model.ChildrenCount,
            Status = OrderStatus.Booked,
            CreationType = OrderCreationType.Web,
            TripId = model.TripId,
            StartTripRoutePointId = model.StartPointId,
            EndTripRoutePointId = model.EndPointId,
            ClientId = GetCurrentUser().Identities.First(r => r.IdentityType == PersonIdentityType.Client).Id,
            Trip = await _tripsService.GetByIdAsync(model.TripId),
            StartTripRoutePoint = await _tripRoutePointsService.GetByIdAsync(model.StartPointId),
            EndTripRoutePoint = await _tripRoutePointsService.GetByIdAsync(model.EndPointId)
        };

        return View(order);
    }
}