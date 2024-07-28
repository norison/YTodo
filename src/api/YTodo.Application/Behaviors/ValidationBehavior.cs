using FluentValidation;

using Mediator;

namespace YTodo.Application.Behaviors;

public class ValidationBehavior<TMessage, TResponse>(IValidator<TMessage> validator)
    : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        await validator.ValidateAndThrowAsync(message, cancellationToken);
        return await next(message, cancellationToken);
    }
}