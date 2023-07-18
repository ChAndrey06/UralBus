using BLL.Interfaces;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.MVC.Models.User;

namespace PL.MVC.Controllers.User;

[Authorize(Roles = nameof(UserRole.None))]
public class ProfileController : Controller
{

    
}