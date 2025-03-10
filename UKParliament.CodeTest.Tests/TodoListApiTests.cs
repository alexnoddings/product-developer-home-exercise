using System.Net;
using System.Net.Http.Json;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Tests;

// For time limitations, only implements tests for Creation
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
    
    [Fact]
    public async Task Create_NullBody_ReturnsBadRequest()
    {
        // Arrange
        var requestContent = JsonContent.Create<CreateTodoRequest?>(null);
        var httpClient = _fixture.CreateApiClient();
        
        // Act
        var response = await httpClient.PostAsync("/todos/", requestContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // should also ensure the body contains an error message which adequately describes the error
    }

    // This should also test other business rules (eg description too long, can't create a completed todo)
    // We could also have additional tests whose sole purpose is validating business rules.
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
        // should also ensure the body contains an error message which adequately describes the error
    }
    
    [Fact]
    public async Task Create_TitleTooLong_ReturnsBadRequest()
    {
        // Arrange
        var title = new string('.', 101);
        var request = new CreateTodoRequest { Title = title };
        var requestContent = JsonContent.Create(request);
        var httpClient = _fixture.CreateApiClient();
        
        // Act
        var response = await httpClient.PostAsync("/todos/", requestContent);
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        // should also ensure the body contains an error message which adequately describes the error
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