using Microsoft.Extensions.Logging;

namespace YTodo.Application.Extensions.Logging;

public static partial class LoggingBehaviorExtensions
{
    [LoggerMessage(1, LogLevel.Debug, "Handling {Message}")]
    public static partial void LogHandlingMessage(this ILogger logger, string message);

    [LoggerMessage(2, LogLevel.Debug, "Handled {Message}")]
    public static partial void LogHandledMessage(this ILogger logger, string message);

    [LoggerMessage(3, LogLevel.Trace, "Tracing Response of {Message}, Response: {Response}", SkipEnabledCheck = true)]
    public static partial void LogResponse(this ILogger logger, string message, string response);

    [LoggerMessage(4, LogLevel.Error, "Error handling {Message}")]
    public static partial void LogHandlingMessageError(this ILogger logger, Exception exception, string message);
}