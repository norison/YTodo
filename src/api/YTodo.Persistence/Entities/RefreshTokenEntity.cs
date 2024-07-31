namespace YTodo.Persistence.Entities;

public class RefreshTokenEntity
{
    public string RefreshToken { get; set; } = string.Empty;

    public int UserId { get; set; }
    public UserEntity? User { get; set; }
}