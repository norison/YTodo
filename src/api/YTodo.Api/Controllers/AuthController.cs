using Mediator;

using Microsoft.AspNetCore.Mvc;

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

        return Ok(new RegisterUserResponse { Id = result.Id });
    }
}