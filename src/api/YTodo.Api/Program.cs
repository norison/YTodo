using Microsoft.EntityFrameworkCore;

using YTodo.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<YTodoDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapControllers();

await app.RunAsync();