namespace YTodo.Application.Services.Token;

public interface ITokenService
{
    string GenerateAccessToken(int userId);
    string GenerateRefreshToken();
}