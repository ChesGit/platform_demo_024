using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data; // Reference to your data context
using PlatformDemo.Web.ViewModels; // Reference to your view models
using System.Threading.Tasks; // For async/await functionality

namespace PlatformDemo.Web.Controllers
{
    // This attribute sets the route for the controller
    [Route("ServicePlans")]
    public class ServicePlansController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to inject the AppDbContext
        public ServicePlansController(AppDbContext context)
        {
            _context = context;
        }

        // Action method to get the list of service plans
        [HttpGet] // This attribute specifies that this method responds to GET requests
        public async Task<IActionResult> Index()
        {
            var servicePlans = await _context.ServicePlans
                .Include(sp => sp.Timesheets)
                .Select(sp => new ServicePlanViewModel
                {
                    ServicePlanId = sp.ServicePlanId,
                    DateOfPurchase = sp.DateOfPurchase,
                    TimesheetCount = sp.Timesheets.Count() // Ensure this is .Count()
                })
                .ToListAsync();

            // Log how many service plans were retrieved
            Console.WriteLine($"Retrieved {servicePlans.Count} service plans.");

            return View(servicePlans);
        }


        // Optionally, you can add more action methods here (e.g., Create, Edit, Delete)
    }
}
