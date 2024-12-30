using Bassma.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bassma.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BakeryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>() // For role management
.AddEntityFrameworkStores<BakeryDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Login page path
    options.AccessDeniedPath = "/Account/AccessDenied"; // Optional for unauthorized access
    options.Events.OnRedirectToReturnUrl = context =>
    {
        context.Response.Redirect(context.RedirectUri); // Prevent default redirect
        return Task.CompletedTask;
    };
});

// Register user and role managers
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enforce HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Enable authentication
app.UseAuthorization(); // Enable authorization

// Map Controller Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Admin/Dashboard",
    defaults: new { controller = "Admin", action = "Index" });

app.MapControllerRoute(
    name: "produits",
    pattern: "Produits/{action=Index}/{id?}",
    defaults: new { controller = "Produits" });

app.MapControllerRoute(
    name: "messages",
    pattern: "Messages/{action=Index}/{id?}",
    defaults: new { controller = "Contact" });

app.MapControllerRoute(
    name: "payments",
    pattern: "Payments/{action=Index}/{id?}",
    defaults: new { controller = "Paiements" });

app.MapControllerRoute(
    name: "roleManagement",
    pattern: "RoleManagement/{action=Index}/{id?}",
    defaults: new { controller = "RoleManagement", action = "Index" }
);

app.MapControllerRoute(
    name: "contact",
    pattern: "Contact/{action=Index}/{id?}",
    defaults: new { controller = "Contact", action = "Index" }
);


app.MapRazorPages(); // Enable Razor Pages

app.Run();