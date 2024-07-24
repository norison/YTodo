using YTodo.Application.Abstractions.UserStorage.Models;

namespace YTodo.Application.Abstractions.UserStorage;

public interface IUserStorage
{
    Task<int> AddUserAsync(AddUserModel model, CancellationToken cancellationToken = default);
}