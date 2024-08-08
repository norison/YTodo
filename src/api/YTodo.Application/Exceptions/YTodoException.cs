namespace YTodo.Application.Exceptions;

public abstract class YTodoException(string message) : Exception(message)
{
    public abstract int Code { get; }
    public abstract string Details { get; }
}