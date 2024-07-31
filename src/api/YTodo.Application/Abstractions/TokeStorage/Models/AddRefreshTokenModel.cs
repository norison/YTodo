namespace YTodo.Application.Abstractions.TokeStorage.Models;

public class AddRefreshTokenModel
{
    public int UserId { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
}