namespace NetCRM.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }  // Dodajemy właściwość 'Title'
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; } // Jeśli chcesz śledzić status zadania
}
