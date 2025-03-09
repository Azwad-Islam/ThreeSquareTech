using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;

namespace RestaurantManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // the value of the query string pageType
        string pageType = Request.Query["pageType"];

        // Set the layout based on the query string value
        if (pageType == "KFC")
        {
            ViewData["Layout"] = "KFCLayout"; // KFC
        }
        else
        {
            ViewData["Layout"] = "BFCLayout"; // BFC
        }

        // Return the view 
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Offers()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
