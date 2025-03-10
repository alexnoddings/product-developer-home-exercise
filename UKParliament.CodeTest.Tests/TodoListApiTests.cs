using System.Net;
using System.Net.Http.Json;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Tests;

// For time limitations, only implements tests for CompleteAsync
// Acts more like an end-to-end test than unit test.
// This should be in a different assembly to unit tests,
// so that the very quick unit tests can be executed independently of the slower e2e/integration tests.
[Collection(WebApiCollection.Name)]
public class TodoListApiTests
{
    private readonly WebApiFixture _fixture;

    public TodoListApiTests(WebApiFixture fixture)
    {
        _fixture = fixture;
    }

    // This should also test other business rules (eg title/description too long, can't create a completed todo)
    [Fact]
    public async Task Create_EmptyTitle_ReturnsBadRequest()
    {
        // Arrange
        var request = new CreateTodoRequest { Title = "" };
        var requestContent = JsonContent.Create(request);
        var httpClient = _fixture.CreateApiClient();
        
        // Act
        var response = await httpClient.PostAsync("/todos/", requestContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // should also ensure the body contains a valid error message
    }

    [Fact]
    public async Task Create_ValidTodo_IsCreated()
    {
        // Arrange
        var request = new CreateTodoRequest { Title = "New todo" };
        var requestContent = JsonContent.Create(request);
        var httpClient = _fixture.CreateApiClient();
        
        // Act
        var response = await httpClient.PostAsync("/todos/", requestContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        // Ensure it returns a valid GUID
        var responseGuid = await response.Content.ReadFromJsonAsync<Guid?>();
        Assert.NotNull(responseGuid);
        Assert.NotEqual(Guid.Empty, responseGuid);
    }
}