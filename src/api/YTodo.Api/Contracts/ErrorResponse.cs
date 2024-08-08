namespace YTodo.Api.Contracts;

public class ErrorResponse
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}