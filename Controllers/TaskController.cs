using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using NetCRM.Models;

[Authorize]
[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public TaskController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var userId = _userManager.GetUserId(User);
        var tasks = await _context.TaskItems // ✅ Zmiana z _context.Tasks na _context.TaskItems
                                   .Where(t => t.UserId == userId)
                                   .ToListAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TaskItem task)
    {
        if (task == null || string.IsNullOrWhiteSpace(task.Title))
            return BadRequest("Nieprawidłowe dane.");

        var userId = _userManager.GetUserId(User);
        task.UserId = userId;

        _context.TaskItems.Add(task); // ✅ Zmiana z _context.Tasks na _context.TaskItems
        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
    {
        if (updatedTask == null || string.IsNullOrWhiteSpace(updatedTask.Title))
            return BadRequest("Nieprawidłowe dane.");

        var task = await _context.TaskItems.FindAsync(id); // ✅ Zmiana z _context.Tasks na _context.TaskItems
        if (task == null || task.UserId != _userManager.GetUserId(User))
            return NotFound();

        task.Title = updatedTask.Title;
        task.IsCompleted = updatedTask.IsCompleted;

        await _context.SaveChangesAsync();
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.TaskItems.FindAsync(id); // ✅ Zmiana z _context.Tasks na _context.TaskItems
        if (task == null || task.UserId != _userManager.GetUserId(User))
            return NotFound();

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
