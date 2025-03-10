using FitnessApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Protects all endpoints in this controller
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }
    [HttpGet("profile")]
    //[Authorize]
    public IActionResult GetUserProfile()
    {
        return Ok(userService.GetUserProfile(User.Identity?.Name));
    }
}
