using EmployeeLeave.Data;
using EmployeeLeave.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeLeave.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        public async Task<IActionResult> Index()
        {
            int pendingUsersCount = await _userManager.Users.CountAsync(u => u.Status == ApprovalStatus.Pending);
            int pendingLeavesCount = await _context.leaves.CountAsync(l => l.Status == "Pending");

            ViewData["PendingUsers"] = pendingUsersCount;
            ViewData["PendingLeaves"] = pendingLeavesCount;

            return View();
        }
    }
}
