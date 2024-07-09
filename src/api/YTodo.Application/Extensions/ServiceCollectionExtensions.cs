using Microsoft.Extensions.DependencyInjection;

namespace YTodo.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}