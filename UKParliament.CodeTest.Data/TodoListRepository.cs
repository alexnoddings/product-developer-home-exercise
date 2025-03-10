using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data;

// This repo implementation doesn't run any validation on input models, as it assumes the service layer has validated properties.
// If any constraints are invalid, the DB model is annotated such that the DB model would (in the case of a real SQL server) fail.
public class TodoListRepository : ITodoListRepository
{
    private readonly TodoListContext _context;

    public TodoListRepository(TodoListContext context)
    {
        _context = context;
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        var todo = await 
            _context
            .TodoItems
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);

        return todo;
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        var todos = await
            _context
                .TodoItems
                .AsNoTracking()
                .ToListAsync();

        return todos;
    }

    public async Task<Guid> CreateAsync(TodoItem todo)
    {
        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task<bool> UpdateAsync(TodoItem todo)
    {
        ArgumentNullException.ThrowIfNull(todo);
        
        var dbTodo = await 
            _context
                .TodoItems
                .FirstOrDefaultAsync(item => item.Id == todo.Id);

        if (dbTodo is null)
            return false;
        
        // Let EfCore handle setting the values for us for ease.
        // This could be done manually to limit what properties a caller can update.
        _context.Entry(dbTodo).CurrentValues.SetValues(todo);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var dbTodo = await 
            _context
                .TodoItems
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == id);

        if (dbTodo is null)
            return false;

        // As an optimisation, we could call .AnyAsync() to determine if the todo exists
        // without transferring it from the db and materialising it in memory,
        // then use _context.Entry to get the EntityEntry, and manually mark it as deleted. 
        _context.TodoItems.Remove(dbTodo);
        await _context.SaveChangesAsync();

        return true;
    }
}