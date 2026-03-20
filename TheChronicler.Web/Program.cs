using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheChronicler.Web.Data;
using TheChronicler.Web.Models;
using TheChronicler.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Database: SQLite for local dev, PostgreSQL for production
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite("Data Source=TheChronicler.db"));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));
}

// Add Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add external authentication (Google, Facebook, Twitter)
// Uncomment and add your OAuth credentials in appsettings.json
// builder.Services.AddAuthentication()
//     .AddGoogle(options =>
//     {
//         options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
//         options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
//     })
//     .AddFacebook(options =>
//     {
//         options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"];
//         options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"];
//     })
//     .AddTwitter(options =>
//     {
//         options.ClientId = builder.Configuration["Authentication:Twitter:ClientId"];
//         options.ClientSecret = builder.Configuration["Authentication:Twitter:ClientSecret"];
//     });

builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

// Add services
builder.Services.AddScoped<ICampaignService, CampaignService>();

// Add Razor Pages with authorization
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Campaigns");
    options.Conventions.AuthorizeFolder("/Sessions");
    options.Conventions.AuthorizeFolder("/Characters");
    options.Conventions.AuthorizeFolder("/Locations");
    options.Conventions.AuthorizeFolder("/Events");
    options.Conventions.AuthorizeFolder("/Timeline");
    options.Conventions.AuthorizeFolder("/Dashboard");
    options.Conventions.AuthorizeFolder("/Forum");
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/Privacy");
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Register");
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

// Seed roles on startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "GameMaster", "Player" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
