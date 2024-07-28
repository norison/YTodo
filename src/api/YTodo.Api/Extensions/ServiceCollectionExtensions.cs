using System.Diagnostics.CodeAnalysis;

using FluentValidation;

using Mediator;

using Microsoft.EntityFrameworkCore;

using YTodo.Application.Abstractions.UserStorage;
using YTodo.Application.Behaviors;
using YTodo.Application.Services.PasswordHasher;
using YTodo.Persistence;
using YTodo.Persistence.Implementations;

namespace YTodo.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Singleton);
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Singleton);
        
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IUserStorage, UserStorage>();
        
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<YTodoDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}