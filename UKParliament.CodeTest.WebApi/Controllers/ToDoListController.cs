using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.WebApi.Controllers;

[Tags("todos")]
[ApiController]
[Route("/todos")]
public class ToDoListController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ITodoListService _todoService;
    // These could be injected into methods if we were using Minimal Api endpoints
    private readonly IValidator<CreateTodoRequest> _createTodoValidator;
    private readonly IValidator<UpdateTodoRequest> _updateTodoValidator;

    public ToDoListController(
        ILogger<ToDoListController> logger,
        ITodoListService todoService,
        IValidator<CreateTodoRequest> createTodoValidator,
        IValidator<UpdateTodoRequest> updateTodoValidator
    )
    {
        _logger = logger;
        _todoService = todoService;
        _createTodoValidator = createTodoValidator;
        _updateTodoValidator = updateTodoValidator;
    }

    // Could use eg [EndpointSummary(...)], [EndpointDescription(...)] to add more OpenAPI-compliant info about these endpoints if for external consumption
    [HttpGet("{id:guid}", Name = "Get todo")]
    // Describes what data/status codes the endpoints can return.
    [ProducesResponseType<TodoModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetByIdAsync([FromRoute] Guid id)
    {
        var model = await _todoService.GetByIdAsync(id);
        if (model is null)
            return Results.NotFound();

        return Results.Ok(model);
    }

    // This implementation exposes the service layer's todo model directly to the API rather than mapping since it's simple
    [HttpGet("", Name = "Get todos")]
    [ProducesResponseType<List<TodoModel>>(StatusCodes.Status200OK)]
    public async Task<IResult> GetAllAsync()
    {
        var models = await _todoService.GetAllAsync();
        return Results.Ok(models);
    }

    [HttpPost("", Name = "Create todo")]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IResult> CreateAsync([FromBody] CreateTodoRequest request)
    {
        // Manual validation is unnecessary boilerplate overhead.
        // But FluentValidation doesn't recommend plumbing into aspnetcore's synchronous validation pipeline,
        // and adding a custom validation middleware seems quite overkill for this task.
        // But said middleware could act as an Endpoint Filter, scan the arguments for anything with eg [FromBody],
        // then pull validation services and automatically return a ProblemDetails for any errors.
        // That way, controller/endpoint methods could assume any model they're given has already been validated.
        var validationResult = await _createTodoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            return Results.ValidationProblem(errors);
        }
        
        var model = request.ToModel();
        var id = await _todoService.CreateAsync(model);
        return Results.Created($"/todos/{id}", id);
    }

    [HttpPost("{id:guid}", Name = "Update todo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateTodoRequest request)
    {
        var validationResult = await _updateTodoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.ToDictionary();
            return Results.ValidationProblem(errors);
        }

        var model = request.ToModel();
        var wasUpdated = await _todoService.UpdateAsync(id, model);

        if (wasUpdated)
            return Results.NoContent();

        return Results.NotFound();
    }
    
    [HttpPost("{id:guid}/complete", Name = "Complete todo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> CompleteAsync([FromRoute] Guid id)
    {
        var wasCompleted = await _todoService.CompleteAsync(id);

        if (wasCompleted)
            return Results.NoContent();

        return Results.NotFound();
    }

    [HttpDelete("{id:guid}", Name = "Delete todo")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> DeleteAsync([FromRoute] Guid id)
    {
        var wasDeleted = await _todoService.DeleteAsync(id);
        
        if (wasDeleted)
            return Results.NoContent();
        
        return Results.NotFound();
    }
}
