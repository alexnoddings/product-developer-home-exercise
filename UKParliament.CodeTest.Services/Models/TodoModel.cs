using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Services.Models;

// Keeps the data and exposed API models separate, so either can be modified/expanded without breaking the other
public class TodoModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }

    public static TodoModel From(TodoItem item) =>
        new()
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            IsCompleted = item.IsCompleted,
            DueDate = item.DueDate,
        };
}