using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Services;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("debug")]
public class DebugController : ControllerBase
{
    private readonly IUserService _userService;

    public DebugController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("user")]
    public IActionResult CreateUser([FromBody] UserRequest newUserRequest)
    {
        var newUser = new UserResponse(_userService.Create(newUserRequest));

        var routeValues = new { userId = newUser.Id };
        
        return CreatedAtAction("GetById", "User", routeValues, newUser);
    }
}