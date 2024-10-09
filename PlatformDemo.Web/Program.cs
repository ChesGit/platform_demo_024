using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data;
using PlatformDemo.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PlatformDemoDb"));

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!dbContext.ServicePlans.Any())
    {
        for (int i = 1; i <= 15; i++)
        {
            var dateOfPurchase = DateTime.Now.AddDays(-i * 2);
            dbContext.ServicePlans.Add(new ServicePlan
            {
                ServicePlanId = i,
                DateOfPurchase = dateOfPurchase
            });

            var random = new Random();
            int timesheetCount = random.Next(0, 6);

            for (int j = 1; j <= timesheetCount; j++)
            {
                dbContext.Timesheets.Add(new Timesheet
                {
                    TimesheetId = (i - 1) * 5 + j,
                    ServicePlanId = i,
                    StartTime = DateTime.Now.AddDays(-i).AddHours(-j),
                    EndTime = DateTime.Now.AddDays(-i).AddHours(-j + 1),
                    Description = $"Timesheet {j} for Service Plan {i}"
                });
            }
        }
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

// Set up a default redirect to the Service Plans page
app.MapGet("/", async context =>
{
    context.Response.Redirect("/ServicePlans");
    await context.Response.CompleteAsync();
});

app.Run();