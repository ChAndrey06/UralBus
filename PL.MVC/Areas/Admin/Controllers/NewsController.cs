using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.RoutePoint;
using PL.Entities.File;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin.Models;
using PL.Services.Interfaces.UserService;
using PL.Services.Admin;
using PL.Entities.News;
using Common.Helpers;

namespace PL.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]/[action]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public class NewsController : BaseMvcController
{
	private readonly NewsService _newsService;

	public NewsController(
		IUserService userService,
		NewsService routePointsService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
		_newsService = routePointsService;
	}

	[HttpGet]
	public async Task<IActionResult> Index(FilterModel filterModel)
	{
		var model = new SearchResultViewModel<News, FilterModel>(
			await _newsService.GetAsync(filterModel),
			filterModel
		);

		return View(model);
	}

	public async Task<IActionResult> Update(Guid? id)
	{
		News? model = new News();

		if (id is not null)
		{
			model = await _newsService.GetByIdAsync((Guid)id);

			if (model is null)
			{
				return NotFound();
			}
		}

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Update(News model)
	{
		var oldNews = await _newsService.GetByIdAsync(model.Id);

		if (model.FilePath is not null)
		{
			model.File = new S3File()
			{
				Path = model.FilePath,
				UploaderId = GetCurrentUser().Id,
				OriginalName = string.Empty,
				DocumentType = DocumentType.News,
				ContentType = string.Empty
			};
		}

		if (oldNews is null)
		{
			await _newsService.AddAsync(model);
		}
		else
		{
			await _newsService.UpdateAsync(model);
		}

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(Guid id)
	{
		await _newsService.DeleteAsync(id);

		return RedirectToAction("Index");
	}
}
