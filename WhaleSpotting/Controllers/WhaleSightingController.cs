using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Services;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("sightings")]
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
    public IActionResult CreateSighting([FromBody] WhaleSightingRequest whaleSightingRequest, [FromHeader(Name = "Authorization")] string authHeader)
    {
        //[FromHeader(Name = "Authorization")]
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
