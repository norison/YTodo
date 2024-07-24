using Mediator;

using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Abstractions.UserStorage.Models;
using YTodo.Application.Services.PasswordHasher;

namespace YTodo.Application.Features.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserStorage userStorage,
    IPasswordHasher passwordHasher) : ICommandHandler<RegisterUserCommand, RegisterUserCommandHandlerResult>
{
    public async ValueTask<RegisterUserCommandHandlerResult> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var model = new AddUserModel
        {
            Email = command.Email,
            FullName = command.FullName,
            PasswordHash = passwordHasher.HashPassword(command.Password)
        };

        var userId = await userStorage.AddUserAsync(model, cancellationToken);

        return new RegisterUserCommandHandlerResult { Id = userId };
    }
}