using System.Security.Claims;

using Mediator;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using YTodo.Api.Contracts.Auth;
using YTodo.Api.Contracts.Auth.RegisterUser;
using YTodo.Application.Features.Commands.RegisterUser;

namespace YTodo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand
        {
            Email = request.Email, Password = request.Password, FullName = request.FullName
        };

        var result = await mediator.Send(command, cancellationToken);
        var response = new AuthResponse { Token = result.Token, ExpirationDateTime = result.ExpirationDateTime };
        
        return Ok(response);
    }
    
    [HttpGet("secure")]
    [Authorize]
    public IActionResult Get()
    {
        var userId = int.Parse(HttpContext.User.FindFirstValue("sub")!);
        
        return Ok($"Hello user with id {userId}");
    }
}