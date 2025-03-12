using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreIdentityDemo.Entity.Tables;
using Microsoft.AspNetCore.Identity;

namespace ASPNETCoreIdentityDemo.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public
            ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
             base(options)
        {

        }
        public DbSet<Employee> employees { get; set; }
    }
}
