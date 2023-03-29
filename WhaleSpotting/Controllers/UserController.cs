using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Models.Database;
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

    [HttpPost("create")]
    public IActionResult Create([FromBody] UserRequest newUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = _userService.Create(newUser);
        var url = Url.Action("GetById", new { userId = user.Id });
        return Created(url, new UserResponse(user));
    }

    [HttpGet("ListUsers")]
    public ActionResult<List<UserResponse>> ListOfUsers()
    {
        try
        {
            var usersList = _userService.ListAllUsers();
            return Ok((usersList));
        }
        catch
        {
            return NotFound();
        }
    }
}
