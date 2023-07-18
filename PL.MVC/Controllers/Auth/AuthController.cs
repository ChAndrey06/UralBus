using System.Security.Claims;
using Common.Enums;
using Common.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PL.Entities.PersonIdentity;
using PL.MVC.Controllers.Base;
using PL.MVC.Models.Auth;
using PL.Services.Admin;
using PL.Services.Interfaces.UserService;

namespace PL.MVC.Controllers.Auth;

[Route("[area]/[controller]/[action]")]
[Area("Public")]
public class AuthController : BaseMvcController
{
    private readonly ISmsService _smsService;

    public AuthController(
        IUserService userService, 
        ISmsService smsService,
		CommonConfigsService commonConfigsService
	) : base(userService, commonConfigsService)
	{
        _smsService = smsService;
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> SmsAuth([FromBody]SmsAuthViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errorList = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(string.Join(System.Environment.NewLine, errorList));
        }


        var user = await _userService.GetUserByPhoneNumberAsync(model.Phone);

        if (user == null)
        {
            return NotFound("UserNotFound");
        }

        var codeValidateResult = await _userService.VerifyCodeAsync(user.PhoneNumber, model.Code, AuthCodeType.Authorization);

        switch (codeValidateResult)
        {
            case ValidateCodeResult.Invalid:
                return BadRequest(codeValidateResult.ToString());

            case ValidateCodeResult.ExpiredByTime:
                return BadRequest(codeValidateResult.ToString());

            case ValidateCodeResult.AlreadyUsed:
                return BadRequest(codeValidateResult.ToString());

            case ValidateCodeResult.Valid:
            {
                var claims = _userService.GetUserClaims(user).ToList();

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                break;
            }
        }

        if (user.Roles.HasFlag(UserRole.None))
        {
            user.Roles=UserRole.Client;
            //confirm role
        }

        return Ok(codeValidateResult.ToString());
    }


    [HttpPost]
    public async Task<IActionResult> RegisterAndSendCode([FromQuery]string phone)
    {
        var userExists = await _userService.CheckUserExistsByPhoneAsync(phone);

        if (userExists)
            return BadRequest("UserAlreadyExist");

        var newUser = new Entities.User.User
        {
            PhoneNumber = phone,
            Roles = UserRole.None |UserRole.Client,
            Identities = new List<PersonIdentity>()
            {
                new ClientPersonIdentity
                {
                    Blacklisted = false,
                }
            }
        };

        newUser = await _userService.CreateUserAsync(newUser);
        var code = await _userService.CreateCodeAsync(newUser.Id, AuthCodeType.Authorization);
        await _smsService.SendSmsAsync(phone, code);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> SendLoginCode([FromQuery]string phone)
    {
        var user = await _userService.GetUserByPhoneNumberAsync(phone);

        if (user == null)
        {
            return NotFound("User with such phone number was not found");
        }

        var code = await _userService.CreateCodeAsync(user.Id, AuthCodeType.Authorization);
        await _smsService.SendSmsAsync(phone, code);

        return Ok();
    }
}
