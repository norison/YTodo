using YTodo.Application.Abstractions.UserStorage.Models;
using YTodo.Domain;

namespace YTodo.Application.Abstractions.UserStorage;

public interface IUserStorage
{
    Task<int> AddUserAsync(AddUserModel model, CancellationToken cancellationToken = default);
    Task<bool> DoesUserExistAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetUserAsync(string email, CancellationToken cancellationToken = default);
}