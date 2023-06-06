using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementMvc.Interface.Services;
using UniversityManagementMvc.Models;

namespace UniversityManagementMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserServices _services;

    public HomeController(ILogger<HomeController> logger,IUserServices userServices)
    {
        _logger = logger;
        _services  =userServices;
    }

    public async Task<IActionResult> Index(string email,string passWord)
    {
        return View(await _services.Login(email,passWord));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
