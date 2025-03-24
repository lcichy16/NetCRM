using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCRM.Data;  // Tw�j kontekst bazy danych, je�li u�ywasz w�asnego

var builder = WebApplication.CreateBuilder(args);

// Dodanie us�ug Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Dodanie us�ug MVC
builder.Services.AddControllersWithViews();

// Konfiguracja bazy danych (przyk�ad dla SQLite, dostosuj do swojego DB)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Konfiguracja middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // W��czenie autentykacji
app.UseAuthorization();   // W��czenie autoryzacji

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();  // W��cza strony Razor (logowanie, rejestracja itp.)

app.Run();
