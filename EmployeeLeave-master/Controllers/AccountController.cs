using EmployeeLeave.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using EmployeeLeave.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using EmployeeLeave.Data.Table;
using Microsoft.EntityFrameworkCore;
using EmployeeLeave.Data;

namespace EmployeeLeave.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserStore<ApplicationUser> userStore,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
             ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
                throw new NotSupportedException("The default UI requires a user store with email support.");

            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            var model = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                Status = ApprovalStatus.Pending //Pending
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var profile = new Profile
                {
                    Name = model.Name,
                    EmployeeId = Guid.Parse(user.Id),
                    Department = null
                };

                _context.profiles.Add(profile);
                await _context.SaveChangesAsync();

                return RedirectToAction("PendingApproval"); //not yet created
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                if (user.Status == ApprovalStatus.Pending)
                {
                    ModelState.AddModelError(string.Empty, "Your account is pending approval.");
                    return View(model);
                }
                if (user.Status == ApprovalStatus.Rejected)
                {
                    ModelState.AddModelError(string.Empty, "Your account has been rejected.");
                    return View(model);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AllEmployee()
        {
            var users = _userManager.Users.ToList();
            _logger.LogInformation("Fetched all users.");
            return View(users);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound("User ID not provided.");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found.");

            var profile = await _context.profiles.FirstOrDefaultAsync(p => p.EmployeeId == Guid.Parse(user.Id));

            var model = new EmployeeViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = profile?.Name ?? "",
                Department = profile?.Department
            };

            return View(model);
        }
        [Authorize] // Only logged-in users can access
        public async Task<IActionResult> PendingUsers()
        {
            // Only approved users can approve/reject others
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Status != ApprovalStatus.Approved)
            {
                return Forbid(); // Block unapproved users from seeing the list
            }

            var pendingUsers = await _userManager.Users
                .Where(u => u.Status == ApprovalStatus.Pending)
                .ToListAsync();
            return View(pendingUsers);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Status != ApprovalStatus.Approved)
            {
                return Forbid(); //unapproved
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Status = ApprovalStatus.Approved;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("PendingUsers");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RejectUser(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Status != ApprovalStatus.Approved)
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.Status = ApprovalStatus.Rejected;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("PendingUsers");
        }
    }

        //    [HttpPost]
        //    [Authorize(Roles = "Admin")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(EmployeeViewModel model)
        //    {
        //        if (!ModelState.IsValid) return View(model);

        //        var user = await _userManager.FindByIdAsync(model.Id);
        //        if (user == null) return NotFound("User not found.");

        //        var profile = await _context.profiles.FirstOrDefaultAsync(p => p.EmployeeId == Guid.Parse(user.Id));

        //        user.Email = model.Email;
        //        await _userManager.UpdateAsync(user);

        //        if (profile != null)
        //        {
        //            profile.Name = model.Name;
        //            profile.Department = model.Department;
        //            _context.profiles.Update(profile);
        //            await _context.SaveChangesAsync();
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }

        //    [Authorize(Roles = "Admin")]
        //    public async Task<IActionResult> Delete(string id)
        //    {
        //        if (string.IsNullOrEmpty(id)) return NotFound("User ID not provided.");

        //        var user = await _userManager.FindByIdAsync(id);
        //        if (user == null) return NotFound("User not found.");

        //        return View(user);
        //    }


        //    [HttpPost, ActionName("Delete")]
        //    [Authorize(Roles = "Admin")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(string id)
        //    {
        //        var user = await _userManager.FindByIdAsync(id);
        //        if (user == null) return NotFound("User not found.");

        //        var profile = await _context.profiles.FirstOrDefaultAsync(p => p.EmployeeId == Guid.Parse(user.Id));

        //        if (profile != null) _context.profiles.Remove(profile);
        //        await _userManager.DeleteAsync(user);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }


        //}
    }
