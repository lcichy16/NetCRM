using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCRM.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Używamy IdentityUser zamiast ApplicationUser
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> TaskItems { get; set; } // ✅ Zbiór tasków użytkowników
}
