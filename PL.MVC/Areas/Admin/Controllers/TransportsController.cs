using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.Transport;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin) + ", " + nameof(UserRole.Dispatcher))]
public class TransportsController : BaseMvcController
{
    private readonly TransportsService _transportsService;
	private readonly KontragentsService _kontragentsService;
	private readonly DriversService _driversService;

	public TransportsController(
        IUserService userService,
        TransportsService transportsService,
		KontragentsService kontragentsService,
		DriversService driversService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
        _transportsService = transportsService;
		_kontragentsService = kontragentsService;
		_driversService = driversService;
	}

    public async Task<IActionResult> Index(FilterModel filterModel)
    {
        var model = new SearchResultViewModel<Transport, FilterModel>(
            await _transportsService.GetAsync(filterModel),
            filterModel
        );

        return View(model);
    }

    public async Task<IActionResult> Update(Guid? id)
    {
        Transport? model = new Transport();

        if (id is not null)
        {
            model = await _transportsService.GetByIdAsync((Guid)id);

            if (model is null)
            {
                return NotFound();
            }
        }

		ViewBag.AllDrivers = await _driversService.GetAsync();
		ViewBag.AllCarriers = await _kontragentsService.GetAsync(KontragentIdentityType.Carrier.ToString());

		return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Transport model)
    {
        var oldTransport = await _transportsService.GetByIdAsync(model.Id);

        if (oldTransport is null)
            await _transportsService.AddAsync(model);
        else
            await _transportsService.UpdateAsync(model);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await _transportsService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}
