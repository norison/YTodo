namespace YTodo.Persistence.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;

    public IEnumerable<RefreshTokenEntity>? RefreshTokens { get; set; }
    public IEnumerable<TodoEntity>? Todos { get; set; }
}