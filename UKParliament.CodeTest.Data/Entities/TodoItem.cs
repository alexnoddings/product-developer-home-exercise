using System.ComponentModel.DataAnnotations;

namespace UKParliament.CodeTest.Data.Entities;

public class TodoItem
{
    public Guid Id { get; set; }
    
    [Required, MinLength(1), MaxLength(100)]
    public string Title { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public DateTime? DueDate { get; set; } 
}