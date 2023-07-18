using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Client.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Controllers.MyTrips;

[Authorize(Roles = nameof(UserRole.Client))]
public class MyTripsController : BaseMvcController
{
    private readonly OrdersService _ordersService;
    
    public MyTripsController(
        IUserService userService,
        OrdersService ordersService,
        PL.Services.Admin.CommonConfigsService commonConfigsService
    ) : base(userService, commonConfigsService)
    {
        _ordersService = ordersService;
    }

    public async Task<IActionResult> IndexAsync(MyTripsFilterModel filterModel)
    {
        var model = new SearchResultViewModel<Entities.Order.Order, MyTripsFilterModel>(
            await _ordersService.GetAsync
            (
                filterModel, 
                GetCurrentUser().Identities.FirstOrDefault
                (
                    r => r.IdentityType == PersonIdentityType.Client
                ).Id
            ),
            filterModel
        );

        return View(model);
    }
}