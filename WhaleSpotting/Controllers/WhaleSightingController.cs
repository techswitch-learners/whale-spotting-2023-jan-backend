using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Services;
using WhaleSpotting.Utilities;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("sightings")]
public class WhaleSightingController : ControllerBase
{
    private readonly IWhaleSightingService _whaleSightingService;
    private readonly ILoginService _loginService;
    public WhaleSightingController(IWhaleSightingService whaleSightingService, ILoginService loginService)
    {
        _whaleSightingService = whaleSightingService;
        _loginService = loginService;
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

    [HttpPatch("{id}/Approve")]
    public ActionResult ApproveSighting([FromRoute] int id, [FromHeader(Name = "Authorization")] string authorization)
    {
        (string Username, string Password) details;

        try
        {
            details = AuthHelper.ExtractFromAuthHeader(authorization);
        }
        catch (Exception)
        {
            return Unauthorized(
                "Authorization header was not valid. Ensure you are using basic auth, and have correctly base64-encoded your username and password.");
        }

        if (_loginService.IsValidLogin(details.Username.ToLower(), details.Password) && _loginService.IsAdmin(details.Username.ToLower()))
        {
            _whaleSightingService.ApproveSighting(id);
            return Ok("Approved");
        }
        return Unauthorized("Invalid login details.");
    }

}
