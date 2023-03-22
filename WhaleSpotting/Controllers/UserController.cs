using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Services;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:int}")]
    public IActionResult GetById([FromRoute] int userId)
    {
        try
        {
            var user = _userService.GetById(userId);
            return Ok(new UserResponse(user));
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }
    [HttpPost("createUser")]
    public IActionResult CreateUser([FromBody] CreateUserRequest newUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = _userService.CreateNewUser(newUser);
        var url = Url.Action("GetById", new { userId = user.Id });
        var responseViewModel = new UserResponse(user);
        return Created(url, responseViewModel);
    }
}
