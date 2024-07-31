using AutoFixture;

using FluentAssertions;

using NSubstitute;

using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Abstractions.UserStorage.Models;
using YTodo.Application.Features.Commands.RegisterUser;
using YTodo.Application.Services.PasswordHasher;

namespace YTodo.Application.Tests.Features.Commands.RegisterUser;

public class RegisterUserCommandHandlerTests : TestBase
{
    // private readonly IUserStorage _userStorage;
    // private readonly IPasswordHasher _passwordHasher;
    // private readonly RegisterUserCommandHandler _handler;
    //
    // public RegisterUserCommandHandlerTests()
    // {
    //     _userStorage = Substitute.For<IUserStorage>();
    //     _passwordHasher = Substitute.For<IPasswordHasher>();
    //     _handler = new RegisterUserCommandHandler(_userStorage, _passwordHasher);
    // }
    //
    // [Fact]
    // public async Task Handle_WhenCalled_ThenAddsUserToStorage()
    // {
    //     // Arrange
    //     var command = Fixture.Create<RegisterUserCommand>();
    //     var userId = Fixture.Create<int>();
    //     var passwordHash = Fixture.Create<string>();
    //     var cancellationToken = Fixture.Create<CancellationToken>();
    //
    //     _passwordHasher.HashPassword(command.Password).Returns(passwordHash);
    //     _userStorage.AddUserAsync(Arg.Any<AddUserModel>(), cancellationToken).Returns(userId);
    //
    //     // Act
    //     var result = await _handler.Handle(command, cancellationToken);
    //
    //     // Assert
    //     result.Id.Should().Be(userId);
    //     
    //     await _userStorage
    //         .Received()
    //         .AddUserAsync(
    //             Arg.Is<AddUserModel>(model =>
    //                 model.Email == command.Email &&
    //                 model.FullName == command.FullName &&
    //                 model.PasswordHash == passwordHash),
    //             cancellationToken);
    // }
}