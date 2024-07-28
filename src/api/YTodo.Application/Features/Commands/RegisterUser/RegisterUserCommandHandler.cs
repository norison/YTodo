using System.Security.Claims;
using System.Text;

using Mediator;

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Abstractions.UserStorage.Models;
using YTodo.Application.Services.PasswordHasher;

namespace YTodo.Application.Features.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserStorage userStorage,
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

        var claims = new List<Claim> { new("sub", userId.ToString()) };

        var expirationDateTime = DateTime.UtcNow.AddHours(1);

        var key = new SymmetricSecurityKey("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"u8.ToArray());
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expirationDateTime,
            SigningCredentials = credentials
        };

        var token = new JsonWebTokenHandler().CreateToken(securityTokenDescriptor);

        return new RegisterUserCommandHandlerResult { Token = token, ExpirationDateTime = expirationDateTime };
    }
}