using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Request;
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

    [HttpPatch("approve/{id}")]
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

    [HttpGet("pending")]
    public IActionResult GetPendingSightings()
    {
        try
        {
            return Ok(_whaleSightingService.GetPendingSightings());
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }

    [HttpGet("")]
    public ActionResult<List<WhaleSightingResponse>> ListApprovedSightings()
    {
        try
        {
            return _whaleSightingService.ListApprovedSightings();
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }

    [HttpPatch("{id}/reject")]
    public IActionResult Reject([FromRoute] int id, [FromHeader(Name = "Authorization")] string authorization)
    {
        if (AuthHelper.LoginChecker(authorization, _loginService))
        {
            _whaleSightingService.RejectId(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] WhaleSightingSearchRequest whaleSightingSearchRequest)
    {
        try
        {
            List<WhaleSightingResponse> whaleSightings = _whaleSightingService.Search(whaleSightingSearchRequest);
            return Ok(whaleSightings);
        }
        catch (SystemException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("submit")]
    public IActionResult CreateSighting([FromBody] WhaleSightingRequest whaleSightingRequest, [FromHeader(Name = "Authorization")] string authHeader)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _whaleSightingService.CreateSighting(whaleSightingRequest, authHeader);
            return Ok($"Your sighting has been created!");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
