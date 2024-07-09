using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace YTodo.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<YTodoDbContext>(options =>
        {
            options.UseInMemoryDatabase("YTodo");
        });

        return services;
    }
}