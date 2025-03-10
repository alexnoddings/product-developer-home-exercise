using FluentValidation;

namespace UKParliament.CodeTest.Services.Models;

public class CreateTodoRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }

    // Mapping is done with an instanced method rather than mapping service for simplicity
    public CreateTodoModel ToModel() =>
        new() { 
            Title = Title, 
            Description = Description, 
            DueDate = DueDate
        };
}

// Validation is all done on the API layer for ease of communicating failures back to the client.
// This could be moved down into the service layer if needed (eg more than one consumer using the services)
public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(r => r.Description)
            .MaximumLength(500);
    }
}