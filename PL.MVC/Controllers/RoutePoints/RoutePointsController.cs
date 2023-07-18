using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Admin.Models;
using PL.Services.Client;
using PL.Services.Client.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Controllers.RoutePoints;

public class RoutePointsController : BaseMvcController
{
    private readonly RoutePointsService _routePointsService;

    public RoutePointsController(
        IUserService userService,
        RoutePointsService routePointsService,
        PL.Services.Admin.CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
        _routePointsService = routePointsService;
    }

    [HttpGet]
    public async Task<IActionResult> Details(FilterModel filterModel) 
    {
        return Ok(await _routePointsService.GetAsync(filterModel));
    }
}
