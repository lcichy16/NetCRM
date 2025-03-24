using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCRM.Data;  // Twój kontekst bazy danych, jeœli u¿ywasz w³asnego

var builder = WebApplication.CreateBuilder(args);

// Dodanie us³ug Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// Dodanie us³ug MVC
builder.Services.AddControllersWithViews();

// Konfiguracja bazy danych (przyk³ad dla SQLite, dostosuj do swojego DB)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Konfiguracja middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // W³¹czenie autentykacji
app.UseAuthorization();   // W³¹czenie autoryzacji

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();  // W³¹cza strony Razor (logowanie, rejestracja itp.)

app.Run();
