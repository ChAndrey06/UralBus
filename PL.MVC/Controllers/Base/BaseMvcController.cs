using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PL.Mappings.MapperFactory;
using PL.Services.Admin;
using PL.Services.Interfaces.UserService;
using BLL.Logic;

namespace PL.MVC.Controllers.Base;


public abstract class BaseMvcController : Controller
{
    protected readonly IMapper _mapper;
    protected readonly IUserService _userService;
    protected readonly CommonConfigsService _commonConfigsService;

    protected BaseMvcController(
        IUserService userService,
        CommonConfigsService commonConfigsService
    )
    {
        _userService = userService;
        _mapper = MapperFactory.GetMapper();
        _commonConfigsService = commonConfigsService;
    }

    /// <summary>
    /// Получить текущего юзера путем десериализации клейма UserData
    /// </summary>
    /// <returns>Текущий юзер либо null</returns>
    protected Entities.User.User? GetCurrentUser()
    {
        var userData = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;

        return JsonConvert.DeserializeObject<Entities.User.User>(userData ?? string.Empty);
    }

    protected async Task UpdateUserClaimsAsync()
    {
        var user = await _userService.GetUserByIdAsync(GetCurrentUser().Id);
        var claims = _userService.GetUserClaims(user).ToList();

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }
    
    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ViewBag.Config = new
        {
            PhoneNumber = (await _commonConfigsService.GetByKeyAsync("PhoneNumber"))?.Value,
            BackgroundImageUrl = (await _commonConfigsService.GetByKeyAsync("HomeBackgroundImageUrl"))?.Value,
            DynamicTitle = (await _commonConfigsService.GetByKeyAsync("HomeDynamicTitle"))?.Value
        };

        await next();
    }
}