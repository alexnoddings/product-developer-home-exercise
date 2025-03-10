using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Services.Models;

public class CreateTodoModel
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }

    // Skip using a mapper since this is so simple.
    public TodoItem ToTodoItem() =>
        new() { 
            Title = Title, 
            Description = Description, 
            DueDate = DueDate
        };
}
