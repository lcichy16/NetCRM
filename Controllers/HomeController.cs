using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCRM.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Akcja ToDo - wyświetla zadania
    public async Task<IActionResult> ToDo()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var tasks = await _context.TaskItems.Where(t => t.UserId == user.Id).ToListAsync();
                return View(tasks); // Zwrócenie widoku ToDo.cshtml
            }
        }
        return RedirectToAction("Index"); // Jeśli użytkownik nie jest zalogowany, wracamy do strony głównej
    }

    // Akcja AddTask - dodaje nowe zadanie
    [HttpPost]
    public async Task<IActionResult> AddTask(string title)
    {
        if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(title))
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var task = new TaskItem
                {
                    Title = title,
                    UserId = user.Id,
                    CreatedAt = DateTime.Now
                };
                _context.TaskItems.Add(task);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("ToDo");
    }

    // Akcja DeleteTask - usuwa zadanie
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task != null)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("ToDo");
    }

    // Akcja Index - strona główna
    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var tasks = await _context.TaskItems
                    .Where(t => t.UserId == user.Id)
                    .ToListAsync();
                return View(tasks);  // Wyświetlamy zadania na stronie głównej
            }
        }

        return View();
    }
    public async Task<IActionResult> MarkComplete(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task != null)
        {
            task.IsCompleted = true; // Oznaczenie zadania jako ukończone
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("ToDo");
    }

}
