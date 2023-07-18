using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Controllers.User;

[Authorize(Roles = nameof(UserRole.Client))]
public class ClientProfileController : BaseMvcController
{

    public ClientProfileController(
        IUserService userService,
        PL.Services.Admin.CommonConfigsService commonConfigsService
    ) : base(userService, commonConfigsService)
    {
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userService.GetClientByIdAsync(GetCurrentUser().Id);
        
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Entities.User.User userModel)
    {
        await _userService.UpdateClientAsync(GetCurrentUser().Id, userModel);
        await UpdateUserClaimsAsync();

        return RedirectToAction("Index");
    }
}