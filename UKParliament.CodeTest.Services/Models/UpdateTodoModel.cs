using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Services.Models;

// Identical implementation to CreateTodoModel for now.
// These are left separate as it's very little duplication overhead, and easily
// allows the models to diverge later if we change what happens during creation/updating
// (eg IsCompleted can be set using Update, but not using Create).
public class UpdateTodoModel
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }

    public TodoItem ToTodoItem() =>
        new() { 
            Title = Title, 
            Description = Description, 
            DueDate = DueDate
        };
}
