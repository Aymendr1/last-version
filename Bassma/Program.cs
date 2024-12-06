using Bassma.Data;
using Microsoft.AspNetCore.Identity;
using System;
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
.AddRoles<IdentityRole>() // If you are using roles
.AddEntityFrameworkStores<BakeryDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Ensure it points to your login action
    options.AccessDeniedPath = "/Account/AccessDenied"; // Optional for unauthorized access
    options.Events.OnRedirectToReturnUrl = context =>
    {
        // Prevent default redirect by handling it manually
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});


builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Home", action = "Dashboard" });

app.MapControllerRoute(
    name: "catalog",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "livraisons",
    pattern: "{controller=Livraisons}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "cart",
    pattern: "{controller=Cart}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "roleManagement",
    pattern: "{controller=RoleManagement}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages(); // Enable Razor Pages
});

app.MapRazorPages();
app.Run();
