using System.Security.Claims;
using System.Text;

using Mediator;

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using YTodo.Application.Abstractions.TokeStorage;
using YTodo.Application.Abstractions.TokeStorage.Models;
using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Abstractions.UserStorage.Models;
using YTodo.Application.Services.PasswordHasher;

namespace YTodo.Application.Features.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserStorage userStorage,
    ITokenStorage tokenStorage,
    IPasswordHasher passwordHasher) : ICommandHandler<RegisterUserCommand, RegisterUserCommandHandlerResult>
{
    public async ValueTask<RegisterUserCommandHandlerResult> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var model = new AddUserModel
        {
            Email = command.Email,
            FullName = command.FullName,
            PasswordHash = passwordHasher.HashPassword(command.Password)
        };

        var userId = await userStorage.AddUserAsync(model, cancellationToken);

        var expirationDateTime = DateTime.UtcNow.AddMinutes(1);
        var accessToken = GenerateAccessToken(userId, expirationDateTime);
        var refreshToken = GenerateRefreshToken();
        
        var refreshTokenModel = new AddRefreshTokenModel
        {
            UserId = userId,
            RefreshToken = refreshToken
        };

        await tokenStorage.AddRefreshTokenAsync(refreshTokenModel, cancellationToken);

        return new RegisterUserCommandHandlerResult
        {
            AccessToken = accessToken, RefreshToken = refreshToken, ExpirationDateTime = expirationDateTime
        };
    }

    private static string GenerateAccessToken(int userId, DateTime expirationDateTime)
    {
        var claims = new List<Claim> { new("sub", userId.ToString()) };

        var key = new SymmetricSecurityKey("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"u8.ToArray());
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), Expires = expirationDateTime, SigningCredentials = credentials
        };

        return new JsonWebTokenHandler().CreateToken(securityTokenDescriptor);
    }

    private static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}