namespace YTodo.Application.Features.Auth;

public class AuthCommandResult
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpirationDateTime { get; set; }
}