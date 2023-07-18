using Microsoft.AspNetCore.Mvc;
using PL.MVC.Controllers.Base;
using PL.Services.Interfaces.UserService;
using PL.Services.Client;
using PL.MVC.Models;
using PL.Services.Admin.Models;

namespace PL.MVC.Controllers.News;

public class NewsController : BaseMvcController
{
    private readonly NewsService _newsService;

    public NewsController(
        IUserService userService,
        PL.Services.Client.NewsService newsService,
        PL.Services.Admin.CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
        _newsService = newsService;
    }

    public async Task<IActionResult> IndexAsync(FilterModel filterModel)
    {
        var model = new SearchResultViewModel<Entities.News.News, FilterModel>(
            await _newsService.GetAsync(filterModel),
            filterModel
        );

        return View(model);
    }
}
