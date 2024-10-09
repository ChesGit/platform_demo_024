using Microsoft.EntityFrameworkCore;
using PlatformDemo.Data;
using PlatformDemo.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // Ensure this line is present
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("PlatformDemoDb")); // Or your actual database configuration

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Check if any service plans exist
    if (!dbContext.ServicePlans.Any())
    {
        // Log or debug to see if seeding is triggered
        Console.WriteLine("No data found, seeding data...");
        dbContext.ServicePlans.AddRange(
            new ServicePlan { ServicePlanId = 1, DateOfPurchase = DateTime.Now.AddDays(-10) },
            new ServicePlan { ServicePlanId = 2, DateOfPurchase = DateTime.Now.AddDays(-5) },
            new ServicePlan { ServicePlanId = 3, DateOfPurchase = DateTime.Now.AddDays(-3) }
        );
        dbContext.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
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
app.MapControllers(); // Ensure this line is present

app.Run();
