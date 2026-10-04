using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------
// MVC + Areas (Admin dashboard lives in Areas/Admin)
// ---------------------------------------------------------------------
builder.Services.AddControllersWithViews();

// ---------------------------------------------------------------------
// EF Core - Database First: the model below maps to the existing
// ZeyyanNajeebDB tables created by Database/PortfolioDB_Script.sql
// ---------------------------------------------------------------------
builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDbConnection")));

// ---------------------------------------------------------------------
// Cookie authentication for the Admin dashboard
// ---------------------------------------------------------------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Account/Login";
        options.AccessDeniedPath = "/Admin/Account/Login";
        options.LogoutPath = "/Admin/Account/Logout";
        options.Cookie.Name = "ZeyyanPortfolio.AdminAuth";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// ---------------------------------------------------------------------
// HTTP pipeline
// ---------------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Admin area routes (e.g. /Admin/Events, /Admin/Account/Login)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Public site routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
