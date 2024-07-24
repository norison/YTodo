namespace YTodo.Application.Abstractions.UserStorage.Models;

public class AddUserModel
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}