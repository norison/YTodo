using Mediator;

namespace YTodo.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommand : ICommand<AuthCommandResult>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}