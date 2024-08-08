using FluentValidation;

using Mediator;

namespace YTodo.Application.Behaviors;

public class ValidationBehavior<TMessage, TResponse>(IEnumerable<IValidator<TMessage>> validators)
    : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        var tasks = validators.Select(validator => validator.ValidateAndThrowAsync(message, cancellationToken));

        await Task.WhenAll(tasks);
        
        return await next(message, cancellationToken);
    }
}