using Microsoft.AspNetCore.Mvc;

public class BFCController : Controller
{
    // This action will return the custom view located in Views/BFC/Index.cshtml
    public IActionResult Index()
    {
        ViewData["Layout"] = "BFCLayout"; // Set BFC layout
        return View();
    }
}
