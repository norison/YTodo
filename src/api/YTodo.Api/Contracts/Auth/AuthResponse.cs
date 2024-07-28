namespace YTodo.Api.Contracts.Auth;

public class AuthResponse
{
    public string Token { get; set; } = String.Empty;
    public DateTime ExpirationDateTime { get; set; }
}