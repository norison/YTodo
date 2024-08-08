namespace YTodo.Application.Exceptions.Auth;

public class UserAlreadyExistsException(string email) : YTodoException("User already exists")
{
    public override int Code => (int)AuthCodes.UserAlreadyExists;
    public override string Details => $"User with email {email} already exists";
}