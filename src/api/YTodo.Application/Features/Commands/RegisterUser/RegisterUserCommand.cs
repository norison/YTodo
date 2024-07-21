using Mediator;

namespace YTodo.Application.Features.Commands.RegisterUser;

public class RegisterUserCommand : ICommand<RegisterUserCommandHandlerResult>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}