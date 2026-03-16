using ClothingMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication; 

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// UPDATED: Strict Cookie Configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.LogoutPath = "/Identity/Account/Logout";

    // By setting these to null/default, we ensure the browser treats it 
    // as a transient session cookie that dies when the browser window closes.
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "ClothingMVC_Session"; // Explicit name helps avoid conflicts
});

var app = builder.Build();

// Database Migration and Admin Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        context.Database.Migrate();

        string adminEmail = "admin@2ft.com";
        string adminPass = "Admin123!";

        var user = userManager.FindByEmailAsync(adminEmail).Result;
        if (user == null)
        {
            var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            userManager.CreateAsync(adminUser, adminPass).Wait();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Database seeding error.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Force sign-out on start-up
// This clears any browser cache when you rerun.
app.Use(async (context, next) =>
{
    if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
    {
        var signInManager = context.RequestServices.GetRequiredService<SignInManager<IdentityUser>>();
        await signInManager.SignOutAsync();
    }
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();