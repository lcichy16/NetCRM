using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCRM.Data;  // Upewnij się, że masz poprawny namespace

var builder = WebApplication.CreateBuilder(args);

// Dodanie usług Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Możesz tutaj skonfigurować opcje Identity, np. wymagania dotyczące haseł
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Dodanie usług MVC
builder.Services.AddControllersWithViews();

// Konfiguracja bazy danych (przykład dla SQLite, dostosuj do swojego DB)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Konfiguracja middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // Włączenie autentykacji
app.UseAuthorization();   // Włączenie autoryzacji

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();  // Włącza strony Razor (logowanie, rejestracja itp.)

app.Run();
