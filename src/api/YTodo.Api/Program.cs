using System.Text;

using FluentValidation;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using YTodo.Api.Contracts;
using YTodo.Api.Extensions;
using YTodo.Application.Exceptions;
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
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
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
        
        ErrorResponse response;

        switch (exception)
        {
            case ValidationException validationException:
                context.Response.StatusCode = 400;
                response = new ErrorResponse
                {
                    Code = 0,
                    Message = "Validation error",
                    Details = validationException.Errors.First().ErrorMessage
                };
                await context.Response.WriteAsJsonAsync(response);
                break;
            case YTodoException yTodoException:
                context.Response.StatusCode = 400;
                response = new ErrorResponse
                {
                    Code = yTodoException.Code,
                    Message = yTodoException.Message,
                    Details = yTodoException.Details
                };
                await context.Response.WriteAsJsonAsync(response);
                break;
            default:
                context.Response.StatusCode = 500;
                response = new ErrorResponse
                {
                    Code = -1,
                    Message = "Internal server error",
                    Details = "An unexpected error occurred"
                };
                await context.Response.WriteAsJsonAsync(response);
                break;
        }
    }
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();