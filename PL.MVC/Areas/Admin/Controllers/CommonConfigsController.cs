using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Admin.Models;
using PL.Services.Admin;
using PL.Services.Interfaces.UserService;
using PL.Entities.CommonConfigs;
using PL.MVC.Models;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class CommonConfigsController : BaseMvcController
{
    public CommonConfigsController(
		IUserService userService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
	}

    public async Task<IActionResult> Index(FilterModel filterModel)
    {
        var model = new SearchResultViewModel<CommonConfiguration, FilterModel>(
            await _commonConfigsService.GetAsync(filterModel),
            filterModel
        );

        return View(model);
    }

    public async Task<IActionResult> Update(Guid? id)
    {
        CommonConfiguration? model = new CommonConfiguration();

        if (id is not null)
        {
            model = await _commonConfigsService.GetByIdAsync((Guid)id);

            if (model is null)
            {
                return NotFound();
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CommonConfiguration model)
    {
        var oldConfig = await _commonConfigsService.GetByIdAsync(model.Id);

        if (oldConfig is null)
            await _commonConfigsService.AddAsync(model);
        else
            await _commonConfigsService.UpdateAsync(model);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _commonConfigsService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}
