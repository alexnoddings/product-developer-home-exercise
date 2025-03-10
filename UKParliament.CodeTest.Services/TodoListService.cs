using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services;

// This implementation doesn't do any validation.
// With the API being our only consumer currently, we assume that layer has performed sufficient validation.
// It's also easier for that layer to translate validation failures into an HTTP response.
// This layer could be expanded to inject validation and throw for invalid data.
public class TodoListService : ITodoListService
{
    private readonly ITodoListRepository _repository;

    public TodoListService(ITodoListRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoModel?> GetByIdAsync(Guid id)
    {
        var todoItem = await _repository.GetByIdAsync(id);
        if (todoItem is null)
            return null;

        var todoModel = TodoModel.From(todoItem);
        return todoModel;
    }

    public async Task<List<TodoModel>> GetAllAsync()
    {
        var todoItems = await _repository.GetAllAsync();
        var todoModels = todoItems.Select(TodoModel.From).ToList();

        return todoModels;
    }

    public async Task<Guid> CreateAsync(CreateTodoModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        
        var todoItem = model.ToTodoItem();
        var createdId = await _repository.CreateAsync(todoItem);
        return createdId;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateTodoModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        
        var todoItem = model.ToTodoItem();
        todoItem.Id = id;
        var wasUpdated = await _repository.UpdateAsync(todoItem);
        return wasUpdated;
    }

    public async Task<bool> CompleteAsync(Guid id)
    {
        var todoItem = await _repository.GetByIdAsync(id);
        if (todoItem is null)
            return false;

        todoItem.IsCompleted = true;
        var wasUpdated = await _repository.UpdateAsync(todoItem);
        return wasUpdated;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var wasDeleted = await _repository.DeleteAsync(id);
        return wasDeleted;
    }
}