namespace YTodo.Application.Exceptions.Auth;

public class UserNotFoundException(string email) : YTodoException("User not found")
{
    public override int Code => (int)AuthCodes.UserNotFound;
    public override string Details => $"User with email {email} not found";
}