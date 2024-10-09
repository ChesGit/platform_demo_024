namespace PlatformDemo.Data.Models
{
    public class ServicePlan
    {
        public int ServicePlanId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public ICollection<Timesheet> Timesheets { get; set; } = new List<Timesheet>();
    }
}
