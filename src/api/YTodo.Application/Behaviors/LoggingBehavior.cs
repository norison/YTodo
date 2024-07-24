using System.Text.Json;

using Mediator;

using Microsoft.Extensions.Logging;

using YTodo.Application.Extensions.Logging;

namespace YTodo.Application.Behaviors;

public class LoggingBehavior<TMessage, TResponse>(ILogger<LoggingBehavior<TMessage, TResponse>> logger)
    : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var typeName = message.GetType().Name;

        try
        {
            logger.LogHandlingMessage(typeName);

            var response = await next(message, cancellationToken);

            logger.LogHandledMessage(typeName);

            return response;
        }
        catch (Exception exception)
        {
            logger.LogHandlingMessageError(exception, typeName);
            throw;
        }
    }
}