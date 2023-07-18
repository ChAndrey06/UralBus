using System.Diagnostics;
using BLL.Entities.Mail;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PL.MVC.Controllers.Base;
using PL.MVC.Models;
using PL.Services.Admin;
using PL.Services.Admin.Models;
using PL.Services.Client;
using PL.Services.Interfaces.UserService;
using AutoMapper;
using BLL.Mappings.MapperFactory;
using PL.Services.UserService;

namespace PL.MVC.Controllers;

public class HomeController : BaseMvcController
{ 
    private readonly ILogger<HomeController> _logger;
    private readonly PopularDestinationsService _popularDestinations;
    private readonly IMailRepository _mailRepository;

    public HomeController(
        ILogger<HomeController> logger, 
        PopularDestinationsService popularDestinations, 
        IMailRepository mailRepository,
        IUserService userService,
        CommonConfigsService commonConfigsService
    ) : base(userService, commonConfigsService)
    {
        _logger = logger;
        _mailRepository = mailRepository;
        _popularDestinations = popularDestinations;
    }

    public async Task<IActionResult> Index()
    {
        var destinations = await _popularDestinations.GetAsync(10);
        ViewBag.PopularDestinations = destinations.Items;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    [HttpPost]
    public IActionResult SendMail(PL.Entities.Mail.Mail mail)
    {
        var bllMail = new Mail  
        { 
            Name = mail.Name,
            Surname = mail.Surname,
            Phone = mail.Phone
        };
        _mailRepository.Send(bllMail);
        return RedirectToAction("Index");
    }
}
