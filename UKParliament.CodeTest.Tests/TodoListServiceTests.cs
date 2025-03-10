using FakeItEasy;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services;

namespace UKParliament.CodeTest.Tests;

// For time limitations, only implements tests for CompleteAsync
public class TodoListServiceTests
{
    [Fact]
    public async Task Complete_InvalidTodo_ReturnsFalse()
    {
        // Arrange
        var todoId = Guid.Parse("63aa13f4-e086-4b4d-b9ad-10d1e7ad1750");
        
        var repo = A.Fake<ITodoListRepository>();
        A.CallTo(() => repo.GetByIdAsync(todoId))
            .Returns(Task.FromResult<TodoItem?>(null));

        var service = new TodoListService(repo);
        
        // Act
        var didComplete = await service.CompleteAsync(todoId);
        
        // Assert
        Assert.False(didComplete);
    }

    [Fact]
    public async Task Complete_ValidTodo_IsCompleted()
    {
        // Arrange
        var todoId = Guid.Parse("42ca6729-35c9-4a20-ac7f-db403d2de7fb");

        var dbTodo = new TodoItem
        {
            Id = todoId,
            Title = "todo title", 
            IsCompleted = false
        };
        
        var repo = A.Fake<ITodoListRepository>();
        A.CallTo(() => repo.GetByIdAsync(todoId))
            .Returns(Task.FromResult<TodoItem?>(dbTodo));

        A.CallTo(() => repo.UpdateAsync(dbTodo))
            .Invokes(call =>
            {
                // This is pretty dumb logic - given more time, it should be
                // expanded to better match the repo and fully update the logic.
                // This would let us properly test the Complete behaviour to ensure
                // it *only* sets the IsCompleted property, and does not modify anything else.
                var item = call.GetArgument<TodoItem>(0)!;
                Assert.Equal(todoId, item.Id);
                dbTodo.IsCompleted = item.IsCompleted;
            })
            .Returns(Task.FromResult(true));

        var service = new TodoListService(repo);
        
        // Act
        var didComplete = await service.CompleteAsync(todoId);
        
        // Assert
        Assert.True(didComplete);
        Assert.True(dbTodo.IsCompleted);
        A.CallTo(() => repo.UpdateAsync(dbTodo))
            .MustHaveHappenedOnceExactly();
    }
}