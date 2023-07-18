using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Client;
using PL.Services.Client.Models;
using PL.Services.Interfaces.UserService;
using PL.MVC.Models;
using PL.Entities.FAQ;

namespace PL.MVC.Controllers.FAQs;

public class FAQsController : BaseMvcController
{
	private readonly FAQsService _fAQsService;

	public FAQsController(
		IUserService userService,
		FAQsService fAQsService,
		PL.Services.Admin.CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_fAQsService = fAQsService;
	}

	public async Task<IActionResult> IndexAsync(Guid categoryId)
	{
		var categories = await _fAQsService.GetCategoriesAsync();
		categoryId = categoryId == Guid.Empty ? categories.Items.FirstOrDefault().Id : categoryId;

        var filterModel = new FAQsFilterModel
		{
			CategoryId = categoryId,
		};

		var model = new SearchResultViewModel<FAQ, FAQsFilterModel>(
			await _fAQsService.GetAsync(filterModel),
			filterModel
		);

		ViewBag.Categories = categories;
		ViewBag.SelectedCategoryId = categoryId;

		return View(model);
	}
}
