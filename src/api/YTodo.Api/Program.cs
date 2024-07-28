using FluentValidation;

using Microsoft.EntityFrameworkCore;

using YTodo.Api.Contracts;
using YTodo.Api.Extensions;
using YTodo.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<YTodoDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception exception)
    {
        Console.WriteLine(exception);

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = 400;
                var response = new ErrorResponse { Message = validationException.Errors.First().ErrorMessage };
                await context.Response.WriteAsJsonAsync(response);
                break;
        }
    }
});

app.MapControllers();

await app.RunAsync();