using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.KontragentIdentity;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin) + ", " + nameof(UserRole.Dispatcher))]
public class KontragentsController : BaseMvcController
{
    private readonly KontragentsService _kontragentsService;

    public KontragentsController(
        IUserService userService,
        KontragentsService kontragentsService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
        _kontragentsService = kontragentsService;
    }

    public async Task<IActionResult> Index(FilterModel filterModel)
    {
        var model = new SearchResultViewModel<KontragentIdentity, FilterModel>(
            await _kontragentsService.GetAsync(filterModel),
            filterModel
        );

        return View(model);
    }

    public async Task<IActionResult> Update(Guid? id)
    {
        KontragentIdentity? model = new KontragentIdentity();

        if (id is not null)
        {
            model = await _kontragentsService.GetByIdAsync((Guid)id);

            if (model is null)
            {
                return NotFound();
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(KontragentIdentity model)
    {
        var oldKontragent = await _kontragentsService.GetByIdAsync(model.Id);

        if (oldKontragent is null)
            await _kontragentsService.AddAsync(model);
        else
            await _kontragentsService.UpdateAsync(model);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _kontragentsService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}
