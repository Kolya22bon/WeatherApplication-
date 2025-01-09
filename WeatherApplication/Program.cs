using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Data; // Пространство имен для ApplicationDbContext
using Microsoft.AspNetCore.Identity;
using WeatherApplication.Models; // Пространство имен для ApplicationUser
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages services
builder.Services.AddRazorPages();

// Configure PostgreSQL context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WeatherAppDbConnection")));

// Add HTTP Client for WeatherService
builder.Services.AddHttpClient<WeatherService>();

// Configure authentication with cookies
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Путь для входа
        options.LogoutPath = "/Account/Logout"; // Путь для выхода
    });

// Add authorization services
builder.Services.AddAuthorization();

// Configure Identity for user management
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Middleware setup
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Root redirection based on authentication
app.MapGet("/", (HttpContext httpContext) =>
{
    if (!httpContext.User.Identity.IsAuthenticated)
    {
        return Results.Redirect("/Account/Register"); // Если не авторизован, редирект на страницу регистрации
    }
    return Results.Redirect("/Index"); // Перенаправление на главную страницу
});

// Map Razor Pages
app.MapRazorPages();

app.Run();
