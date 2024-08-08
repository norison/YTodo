using Mediator;

using YTodo.Application.Abstractions.TokeStorage;
using YTodo.Application.Abstractions.TokeStorage.Models;
using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Exceptions.Auth;
using YTodo.Application.Services.PasswordHasher;
using YTodo.Application.Services.Token;

namespace YTodo.Application.Features.Auth.Commands.LoginUser;

public class LoginUserCommandHandler(
    IUserStorage userStorage,
    ITokenStorage tokenStorage,
    ITokenService tokenService,
    IPasswordHasher passwordHasher) : ICommandHandler<LoginUserCommand, AuthCommandResult>
{
    public async ValueTask<AuthCommandResult> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userStorage.GetUserAsync(command.Email, cancellationToken);
        
        if (user is null || !passwordHasher.VerifyPassword(command.Password, user.PasswordHash))
        {
            throw new UserNotFoundException(command.Email);
        }
        
        var accessToken = tokenService.GenerateAccessToken(user.Id);
        var refreshToken = tokenService.GenerateRefreshToken();
        
        var refreshTokenModel = new AddRefreshTokenModel
        {
            UserId = user.Id,
            RefreshToken = refreshToken
        };
        
        await tokenStorage.AddRefreshTokenAsync(refreshTokenModel, cancellationToken);
        
        return new AuthCommandResult
        {
            AccessToken = accessToken, RefreshToken = refreshToken
        };
    }
}