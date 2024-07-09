using Microsoft.AspNetCore.Mvc;

namespace YTodo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok("Hello from UsersController");
    }
}