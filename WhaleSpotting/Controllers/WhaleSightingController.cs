using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Services;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("sighting")]
public class WhaleSightingController : ControllerBase
{
    private readonly IWhaleSightingService _whaleSightingService;
    public WhaleSightingController(IWhaleSightingService whaleSightingService)
    {
        _whaleSightingService = whaleSightingService;
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetById([FromRoute] int Id)
    {
        try
        {
            var whaleSighting = _whaleSightingService.GetById(Id);
            return Ok(new WhaleSightingResponse(whaleSighting));
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }

    [HttpPost("submit")]
    public IActionResult CreateSighting([FromRoute] WhaleSightingRequest whaleSightingRequest, string authHeader)
    {
        //[FromHeader(Name = "Authorization")]
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _whaleSightingService.CreateSighting(whaleSightingRequest, authHeader);
            return Ok($"TBC");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.InnerException);
        }
    }
}

//from Controller
/*
[HttpPost("create")]
    public IActionResult Create([FromBody] CreateUserRequest newUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _users.Create(newUser);

        var url = Url.Action("GetById", new { id = user.Id });
        var responseViewModel = new UserResponse(user);
        return Created(url, responseViewModel);
    }
    */

//from Repositroies 
/*
public User Create(CreateUserRequest newUser)
        {
            byte[] ourSalt = UsersRepo.createSalt();
            var ourHashedPassword = UsersRepo.createHashedPassword(newUser.UserAddedPassword, ourSalt);
            var insertResponse = _context.Users.Add(new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Username = newUser.Username,
                Salt = ourSalt,
                Hashed_Password = ourHashedPassword,
                ProfileImageUrl = newUser.ProfileImageUrl,
                CoverImageUrl = newUser.CoverImageUrl,
            });
            _context.SaveChanges();

            return insertResponse.Entity;
        }
        */


