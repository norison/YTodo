using YTodo.Application.Abstractions.TokeStorage.Models;

namespace YTodo.Application.Abstractions.TokeStorage;

public interface ITokenStorage
{
    Task AddRefreshTokenAsync(AddRefreshTokenModel model, CancellationToken cancellationToken = default);
    Task<string?> GetRefreshTokenAsync(int userId, CancellationToken cancellationToken = default);
}