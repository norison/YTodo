using Microsoft.EntityFrameworkCore;

using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Abstractions.UserStorage.Models;
using YTodo.Domain;
using YTodo.Persistence.Entities;

namespace YTodo.Persistence.Implementations;

public class UserStorage(IDbContextFactory<YTodoDbContext> dbContextFactory) : IUserStorage
{
    public async Task<int> AddUserAsync(AddUserModel model, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var user = new UserEntity { Email = model.Email, PasswordHash = model.PasswordHash, FullName = model.FullName };

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }

    public async Task<bool> DoesUserExistAsync(string email, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserAsync(string email, CancellationToken cancellationToken = default)
    {
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        return user is null ? null : new User { Id = user.Id, Email = user.Email, PasswordHash = user.PasswordHash, FullName = user.FullName };
    }
}