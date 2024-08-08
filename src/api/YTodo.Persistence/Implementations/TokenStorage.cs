using Microsoft.EntityFrameworkCore;

using YTodo.Application.Abstractions.TokeStorage;
using YTodo.Application.Abstractions.TokeStorage.Models;
using YTodo.Persistence.Entities;

namespace YTodo.Persistence.Implementations;

public class TokenStorage(IDbContextFactory<YTodoDbContext> dbContextFactory) : ITokenStorage
{
    public async Task AddRefreshTokenAsync(AddRefreshTokenModel model, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var oldRefreshToken = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == model.UserId, cancellationToken);
        
        if(oldRefreshToken is not null)
        {
            oldRefreshToken.RefreshToken = model.RefreshToken;
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        var refreshToken = new RefreshTokenEntity { UserId = model.UserId, RefreshToken = model.RefreshToken };

        await dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<string?> GetRefreshTokenAsync(int userId, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var refreshToken =
            await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        
        return refreshToken?.RefreshToken;
    }
}