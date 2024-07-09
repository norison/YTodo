using YTodo.Application.Extensions;
using YTodo.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication().AddPersistence();

var app = builder.Build();

await app.RunAsync();