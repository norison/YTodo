using System.Text;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using YTodo.Api.Contracts;
using YTodo.Api.Extensions;
using YTodo.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer("Bearer", options =>
{
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"u8.ToArray())
    };
});


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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();