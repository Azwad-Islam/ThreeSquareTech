using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class LeaveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
