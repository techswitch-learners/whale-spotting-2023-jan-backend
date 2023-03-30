using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Database;
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
    public WhaleSightingController(IWhaleSightingService whaleSightingService,ILoginService loginService)
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
             return  _whaleSightingService.ListApprovedSightings();
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }
    
    [HttpPatch("{id}/reject")]
    public IActionResult Reject([FromRoute] int id, [FromHeader(Name = "Authorization")] string authorization) {
        if(AuthHelper.LoginChecker(authorization, _loginService))
        {
            _whaleSightingService.RejectId(id);
            return Ok();
        } else {
            return NotFound();
        }
    }
}
