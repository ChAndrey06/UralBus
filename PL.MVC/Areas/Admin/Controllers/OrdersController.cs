using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Admin.Models;

using PL.Services.Interfaces.UserService;
using PL.MVC.Models;
using PL.Entities.Order;
using System;
using System.Threading.Tasks;
using Common.Configuration;
using PL.Services.Admin;
using PL.Services.Client.Models;
using PL.Services.UserService;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin) + ", " + nameof(UserRole.Operator))]
public class OrdersController : BaseMvcController
{
    private readonly OrdersService _ordersService;
    private new readonly UserService _userService;
    private readonly UsersService _usersService;
    private readonly PL.Services.Client.TripsService _tripsService;

    public OrdersController(
        UsersService usersService,
        IUserService userService,
        OrdersService ordersService,
        CommonConfigsService commonConfigsService, PL.Services.Client.TripsService tripsService, UserService userService1) : base(userService, commonConfigsService)
    {
        _ordersService = ordersService;
        _tripsService = tripsService;
        _userService = userService1;
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(FilterModel filterModel)
    {
        var model = new SearchResultViewModel<Order, FilterModel>(await _ordersService.GetAsync(filterModel), filterModel);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid? id)
    {
        Order order = null;
        if (id.HasValue)
            order = await _ordersService.GetByIdAsync(id.Value);

        return View(order);
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
            await _usersService.UpdateAsync(order.Client.User);
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

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _ordersService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPointsByTripId(Guid id)
    {
        var points = await _tripsService.GetPointsByTripIdAsync(id, true);

        if (points == null)
        {
            return NotFound();
        }

        return Ok(points.Cast<DetaliedTripRoutePointDataDto>());
    }
    
    public async Task<IActionResult> Refund(Guid id)
    {
        var order = await _ordersService.GetByIdAsync(id);
        //order.ClientPhoneNumber = order.Client.PhoneNumber;
        return View(order);
    }
    
    [HttpPost]
    public async Task<IActionResult> Refund(Order order)
    {
        if (string.IsNullOrEmpty(order.ReasonRefund))
        {
            ModelState.AddModelError(nameof(order.ClientPhoneNumber),"Укажите причину");
            return View(order);
        }

        var userByPhoneNumberAsync = await _userService.GetUserByPhoneNumberAsync(order.ClientPhoneNumber);
        var client = userByPhoneNumberAsync?.Identities?.FirstOrDefault(r=>r.IdentityType==PersonIdentityType.Client);
        if (client == null)
        {
            ModelState.AddModelError(nameof(order.ClientPhoneNumber),"Данный клиент не существует или не является клиентом");
            return View(order);
        }

        order.ClientId = client.Id;

        var trip = _tripsService.GetByIdAsync(order.TripId);
        //@TODO add check for points order
        if (trip == null)
        {
            ModelState.AddModelError(nameof(order.TripId),"Данный рейс не существует");
            return View(order);
        }
        
        await _ordersService.UpdateAsync(order);
        return RedirectToAction("Index");
    }
}