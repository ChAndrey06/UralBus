using Common.Configuration;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.Order;
using PL.Entities.Trip;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Client.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Areas.Admin.Controllers;


[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin) + "," + nameof(UserRole.Operator))]
public class CreateOrderController : BaseMvcController
{
    private readonly TripsService _tripsService;
    private readonly OrdersService _ordersService;
    private readonly UsersService _usersService;
    private readonly PL.Services.Client.TripsService _tripsServiceClient;

    public CreateOrderController(
        IUserService userService,
        UsersService usersService,
        TripsService tripsService,
        OrdersService ordersService,
        PL.Services.Client.TripsService tripsServiceClient,
        CommonConfigsService commonConfigsService
    ) : base(userService, commonConfigsService)
    {
        _tripsService = tripsService; 
        _ordersService = ordersService;
        _tripsServiceClient = tripsServiceClient;
        _usersService = usersService;
    }

    public async Task<IActionResult> Index(TripFilterModel tripFilterModel)
    {
        var trips = new SearchResultViewModel<Trip, TripFilterModel>(
            await _tripsService.GetWithTripFilterAsync(tripFilterModel), tripFilterModel );

        return View(trips);
    }
    
    public async Task<IActionResult> Update(Guid id)
    {
        var trip = await _tripsService.GetByIdAsync(id);

        return View(new Order
        {
            Trip = trip
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(Order order)
    {
        var userByPhoneNumberAsync = await _userService.GetUserByPhoneNumberAsync(order.ClientPhoneNumber);
        
        var client = userByPhoneNumberAsync?.Identities?.FirstOrDefault(r=>r.IdentityType==PersonIdentityType.Client);

        if (client == null)
        {
            if (string.IsNullOrEmpty(order.ClientFirstName)
                || string.IsNullOrEmpty(order.ClientLastName)
                || string.IsNullOrEmpty(order.ClientPatronymic))
            {
                ModelState.AddModelError(nameof(order.TripId),"Данного клиента не существует, укажите ФИО");
                order.Trip = await _tripsService.GetByIdAsync(order.TripId);
                return View(order);
            }

            order.ClientId = Constants.DefaultClientId;
        }
        else
        {
            order.ClientId = client.Id;
        }

        var trip = _tripsService.GetByIdAsync(order.TripId);
        //@TODO add check for points order
        if (trip == null)
        {
            ModelState.AddModelError(nameof(order.TripId),"Данный рейс не существует");
            return View(order);
        }

        if (order.Id == Guid.Empty)
            await _ordersService.AddAsync(order);
        else
            await _ordersService.UpdateAsync(order);

        return RedirectToAction("Index","Orders");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPointsByTripId(Guid id)
    {
        var points = await _tripsServiceClient.GetPointsByTripIdAsync(id, true);

        if (points == null)
        {
            return NotFound();
        }

        return Ok(points.Cast<DetaliedTripRoutePointDataDto>());
    }
}