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
public class FAQCategoriesController : BaseMvcController
{
	private readonly FAQCategoriesService _fAQCategoriesService;

	public FAQCategoriesController(
		IUserService userService,
		FAQCategoriesService fAQCategoriesService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_fAQCategoriesService = fAQCategoriesService;
	}

	[HttpGet]
	public async Task<IActionResult> Index(FilterModel filterModel)
	{
		var model = new SearchResultViewModel<FAQCategory, FilterModel>(
			await _fAQCategoriesService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	public async Task<IActionResult> Update(Guid? id)
	{
		FAQCategory? model = new FAQCategory();

		if (id is not null)
		{
			model = await _fAQCategoriesService.GetByIdAsync((Guid)id);

			if (model is null)
			{
				return NotFound();
			}
		}

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Update(FAQCategory model)
	{
		var oldRoutePoint = await _fAQCategoriesService.GetByIdAsync(model.Id);

		if (oldRoutePoint is null)
			await _fAQCategoriesService.AddAsync(model);
		else
			await _fAQCategoriesService.UpdateAsync(model);

		return RedirectToAction("Index", "FAQs");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _fAQCategoriesService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
