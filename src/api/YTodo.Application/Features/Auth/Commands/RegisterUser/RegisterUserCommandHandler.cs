using System.Transactions;

using Mediator;

using YTodo.Application.Abstractions.TokeStorage;
using YTodo.Application.Abstractions.TokeStorage.Models;
using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Abstractions.UserStorage.Models;
using YTodo.Application.Exceptions.Auth;
using YTodo.Application.Services.PasswordHasher;
using YTodo.Application.Services.Token;

namespace YTodo.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserStorage userStorage,
    ITokenStorage tokenStorage,
    ITokenService tokenService,
    IPasswordHasher passwordHasher) : ICommandHandler<RegisterUserCommand, AuthCommandResult>
{
    public async ValueTask<AuthCommandResult> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var doesUserExist = await userStorage.DoesUserExistAsync(command.Email, cancellationToken);

        if (doesUserExist)
        {
            throw new UserAlreadyExistsException(command.Email);
        }
        
        var model = new AddUserModel
        {
            Email = command.Email,
            FullName = command.FullName,
            PasswordHash = passwordHasher.HashPassword(command.Password)
        };

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var userId = await userStorage.AddUserAsync(model, cancellationToken);

        var accessToken = tokenService.GenerateAccessToken(userId);
        var refreshToken = tokenService.GenerateRefreshToken();
        
        var refreshTokenModel = new AddRefreshTokenModel
        {
            UserId = userId,
            RefreshToken = refreshToken
        };

        await tokenStorage.AddRefreshTokenAsync(refreshTokenModel, cancellationToken);
        
        transactionScope.Complete();
            
        return new AuthCommandResult
        {
            AccessToken = accessToken, RefreshToken = refreshToken
        };
    }
}