using Microsoft.AspNetCore.Mvc;
using NetCRM.Data;
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList(); // Przyk�ad pobierania u�ytkownik�w
        return View(users);
    }
}
