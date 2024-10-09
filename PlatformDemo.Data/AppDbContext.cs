// AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data.Models;

namespace PlatformDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ServicePlan> ServicePlans { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }

        // Remove OnModelCreating if you are handling seeding in Program.cs
    }
}
