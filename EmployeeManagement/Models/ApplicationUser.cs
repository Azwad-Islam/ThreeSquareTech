using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Models
{
    // Inherit from IdentityUser to add custom properties to your user
    public class ApplicationUser : IdentityUser
    {
        // You can add custom properties here (optional)
        // Example: 
         public string FullName { get; set; }
    }
}
