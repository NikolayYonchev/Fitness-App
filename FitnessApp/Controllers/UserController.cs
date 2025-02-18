using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Protects all endpoints in this controller
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [HttpGet("profile")]
    public IActionResult GetUserProfile()
    {
        var userName = User.Identity?.Name; // Get authenticated user's name
        return Ok(new { message = $"Hello, {userName}! This is your secure profile." });
    }
}
