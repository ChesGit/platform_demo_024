using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data.Models;

namespace PlatformDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ServicePlan> ServicePlans { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Service Plans
            modelBuilder.Entity<ServicePlan>().HasData(
                new ServicePlan { ServicePlanId = 1, DateOfPurchase = DateTime.Now.AddDays(-10) },
                new ServicePlan { ServicePlanId = 2, DateOfPurchase = DateTime.Now.AddDays(-5) },
                new ServicePlan { ServicePlanId = 3, DateOfPurchase = DateTime.Now.AddDays(-3) }
            );

            // Seed data for Timesheets
            modelBuilder.Entity<Timesheet>().HasData(
                new Timesheet { TimesheetId = 1, ServicePlanId = 1, StartTime = DateTime.Now.AddDays(-9), EndTime = DateTime.Now.AddDays(-9).AddHours(1), Description = "Setup" },
                new Timesheet { TimesheetId = 2, ServicePlanId = 1, StartTime = DateTime.Now.AddDays(-8), EndTime = DateTime.Now.AddDays(-8).AddHours(2), Description = "Configuration" }
            );
        }
    }
}
