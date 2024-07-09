using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using YTodo.Api.Controllers;

namespace YTodo.Api.Tests.Controllers;

[TestFixture]
public class UsersControllerTests
{
    [Test]
    public void GetUsers_WhenCalled_ThenShouldReturnOk()
    {
        // Arrange
        var controller = new UsersController();

        // Act
        var result = controller.GetUsers();

        // Assert
        result
            .Should()
            .BeOfType<OkObjectResult>()
            .Which.Value.Should().Be("Hello from UsersController");
    }
}