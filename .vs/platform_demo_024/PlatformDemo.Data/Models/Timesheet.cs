namespace PlatformDemo.Data.Models
{
	public class Timesheet
	{
		public int TimesheetId { get; set; }
		public int ServicePlanId { get; set; }
		public ServicePlan ServicePlan { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Description { get; set; }
	}
}
