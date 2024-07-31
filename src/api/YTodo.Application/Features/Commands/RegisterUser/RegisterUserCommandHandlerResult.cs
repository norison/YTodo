namespace YTodo.Application.Features.Commands.RegisterUser;

public class RegisterUserCommandHandlerResult
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpirationDateTime { get; set; }
}