using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Controllers.PassengerData;

[Authorize(Roles = nameof(UserRole.Client))]
public class PassengersDataController : BaseMvcController
{
    private readonly PassengersDataService _passengersDataService;
    
    public PassengersDataController(
        IUserService userService,
        PassengersDataService passengersDataService,
        PL.Services.Admin.CommonConfigsService commonConfigsService
    ) : base(userService, commonConfigsService)
    {
        _passengersDataService = passengersDataService;
    }
    
    [HttpGet]
    public async Task<IActionResult> IndexAsync(FilterModel filterModel)
    {
        var model = new SearchResultViewModel<PL.Entities.PassengerData.PassengerData, FilterModel>(
            await _passengersDataService.GetAsync(filterModel),
            filterModel
        );

        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _passengersDataService.DeleteAsync(id);
        
        return RedirectToAction("Index");
    }
}