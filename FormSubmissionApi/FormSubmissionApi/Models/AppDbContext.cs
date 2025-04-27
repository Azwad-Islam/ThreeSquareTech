using Microsoft.EntityFrameworkCore;

namespace FormSubmissionApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FormData> FormDatas { get; set; }
        public DbSet<ValidCode> ValidCodes { get; set; }
    }
}
