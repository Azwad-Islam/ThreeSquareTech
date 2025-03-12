using Microsoft.AspNetCore.Mvc;

public class KFCController : Controller
{
    // This action will return the custom view located in Views/BFC/Index.cshtml
    public IActionResult Index()
    {
        ViewData["Layout"] = "KFCLayout"; // Set BFC layout
        return View();
    }
}
