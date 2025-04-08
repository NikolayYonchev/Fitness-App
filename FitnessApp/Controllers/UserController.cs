using FitnessApp.Data;
using FitnessApp.Models;
using FitnessApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

[Authorize] // Protects all endpoints in this controller
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    //private readonly ApplicationDbContext _context;


    public UserController(IUserService userService/*, ApplicationDbContext _context*/)
    {
        this.userService = userService;
        //this._context = _context;
    }
    [HttpGet("profile")]
    //[Authorize]
    public IActionResult GetUserProfile()
    {
        return Ok(userService.GetUserProfile(User.Identity?.Name));
    }

}
