using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.FAQ;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class FAQsController : BaseMvcController
{
	private readonly FAQsService _fAQsService;
	private readonly FAQCategoriesService _fAQCategoriesService;

	public FAQsController(
		IUserService userService,
		FAQsService fAQsService,
		FAQCategoriesService fAQCategoriesService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_fAQsService = fAQsService;
		_fAQCategoriesService = fAQCategoriesService;
	}

    public async Task<IActionResult> Index(FilterModel filterModel)
    {
		var model = new SearchResultViewModel<FAQ, FilterModel>(
			await _fAQsService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	public async Task<IActionResult> Update(Guid? id)
	{
		FAQ? model = new FAQ();

		if (id is not null)
		{
			model = await _fAQsService.GetByIdAsync((Guid)id);

			if (model is null)
			{
				return NotFound();
			}
		}

		ViewBag.AllCategories = await _fAQCategoriesService.GetAsync();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Update(FAQ model)
	{
		var oldFAQ = await _fAQsService.GetByIdAsync(model.Id);

		if (oldFAQ is null)
			await _fAQsService.AddAsync(model);
		else
			await _fAQsService.UpdateAsync(model);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _fAQsService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
