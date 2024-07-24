using Microsoft.EntityFrameworkCore;

using YTodo.Api.Extensions;
using YTodo.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<YTodoDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.MapControllers();

await app.RunAsync();