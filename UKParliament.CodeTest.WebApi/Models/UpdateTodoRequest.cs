using FluentValidation;

namespace UKParliament.CodeTest.Services.Models;

// Identical implementation to CreateTodoRequest for now.
// These are left separate as it's very little duplication overhead, and easily
// allows the models to diverge later if we change what happens during creation/updating
// (eg IsCompleted can be set using Update, but not using Create).
public class UpdateTodoRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }

    public UpdateTodoModel ToModel() =>
        new() { 
            Title = Title, 
            Description = Description, 
            DueDate = DueDate
        };
}

public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
{
    public UpdateTodoRequestValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(r => r.Description)
            .MaximumLength(500);
    }
}
