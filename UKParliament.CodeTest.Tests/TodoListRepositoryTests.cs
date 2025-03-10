using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Tests;

// For time limitations, only implements tests for UpdateAsync
public class TodoListRepositoryTests
{
    [Fact]
    public async Task Update_NullTodo_ThrowsArgumentNullException()
    {
        await using var dbContext = CreateDbContext();
        var repo = new TodoListRepository(dbContext);
        
        // Arrange
        TodoItem? item = null;

        // Act
        Task<bool> Act() => repo.UpdateAsync(item!);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(Act);
    }
    
    [Fact]
    public async Task Update_InvalidTodoItem_ReturnsFalse()
    {
        // Arrange
        var todoId = Guid.Parse("6c8a954b-628e-45df-92de-b98e387de001");
        
        await using var dbContext = CreateDbContext();
        var repo = new TodoListRepository(dbContext);

        // Act
        var todo = new TodoItem
        {
            Id = todoId,
            Title = "Updated todo title",
            Description = "Updated todo description",
            DueDate = DateTime.Parse("10/03/2025 17:22"),
            IsCompleted = true
        };
        var updated = await repo.UpdateAsync(todo);
        
        // Assert
        Assert.False(updated);
    }
    
    [Fact]
    public async Task Update_ExistingTodoItem_IsUpdated()
    {
        // Arrange
        var todoId = Guid.Parse("65384bb0-05e6-474a-86a9-2c0b0d0aff55");
        
        var newTodo = new TodoItem { Id = todoId, Title = "Test todo" };
        await CreateTodoAsync(newTodo);
        
        await using var dbContext = CreateDbContext();
        var repo = new TodoListRepository(dbContext);

        // Act
        var todo = new TodoItem
        {
            Id = todoId,
            Title = "Updated todo title",
            Description = "Updated todo description",
            DueDate = DateTime.Parse("10/03/2025 17:22"),
            IsCompleted = true
        };
        var updated = await repo.UpdateAsync(todo);
        
        // Assert
        Assert.True(updated);
        var dbTodo = await GetTodoAsync(todoId);
        Assert.NotNull(dbTodo);
        Assert.Equal(todo.Title, dbTodo.Title);
        Assert.Equal(todo.Description, dbTodo.Description);
        Assert.Equal(todo.DueDate, dbTodo.DueDate);
        Assert.Equal(todo.IsCompleted, dbTodo.IsCompleted);
    }

    // Helper methods
    private static TodoListContext CreateDbContext()
    {
        // If this was strictly a unit test, we'd mock the dbcontext rather than new-ing one up.
        // But that hides a lot of hidden complexity with EF, and is quite time-consuming to mock for this exercise.
        
        // Ideally for an integration test, this would use something like test containers
        // to make sure we're testing all the way down to the database level.
        var optionsBuilder = new DbContextOptionsBuilder<TodoListContext>();
        optionsBuilder.UseInMemoryDatabase("tests");
        var options = optionsBuilder.Options;
        var dbContext = new TodoListContext(options); 
        return dbContext;
    }
    
    private static async Task CreateTodoAsync(TodoItem todo)
    {
        await using var dbContext = CreateDbContext();
        dbContext.TodoItems.Add(todo);
        await dbContext.SaveChangesAsync();
    }

    private static async Task<TodoItem?> GetTodoAsync(Guid id)
    {
        await using var dbContext = CreateDbContext();
        return await dbContext.TodoItems.AsNoTracking().FirstOrDefaultAsync(todo => todo.Id == id);
    }
}