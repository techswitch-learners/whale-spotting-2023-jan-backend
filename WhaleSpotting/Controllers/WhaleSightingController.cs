using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Database;
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
}
