using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Data;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// 1. Configure Services
// ---------------------------------------------------------------------------

// Configure EF Core with SQL Server using the connection string from appsettings.json.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Set up ASP.NET Core Identity with role support.
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()  // <-- Enable roles (so RoleManager is registered)
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add MVC and apply a global authorization policy: all controllers require an authenticated user by default.
// Identity pages have [AllowAnonymous] attributes so they remain accessible.
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.AddRazorPages(options =>
{
    // Allow anonymous access to all pages in the Identity area.
    options.Conventions.AllowAnonymousToAreaFolder("Identity", "/Account");
});

var app = builder.Build();

// ---------------------------------------------------------------------------
// 2. Seed Roles and a Default Admin User
// ---------------------------------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Define the roles to seed.
    string[] roles = { "Admin", "Agent", "Client" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Seed a default Admin user if one doesn't exist.
    string adminEmail = "admin@yourdomain.com";
    string adminPassword = "Password123!"; // Ensure you use a strong password in production.
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// ---------------------------------------------------------------------------
// 3. Configure the HTTP Request Pipeline
// ---------------------------------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication must come before Authorization.
app.UseAuthentication();
app.UseAuthorization();

// Map default controller route.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map Razor Pages (includes Identity pages).
app.MapRazorPages();

// Start the application.
await app.RunAsync();
