using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Database;
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

    [HttpGet("allpending")]
    public IActionResult GetPendingSightings()
    {
        List<WhaleSighting> whaleSightingsPendingList = _whaleSightingService.GetPendingSightings();
        List<WhaleSightingResponse> whaleSightingsPendingListResponses = new List<WhaleSightingResponse>();

        foreach (WhaleSighting ws in whaleSightingsPendingList)
        {
            var ourWsResponse = new WhaleSightingResponse(ws);
            whaleSightingsPendingListResponses.Add(ourWsResponse);
        }
        return Ok(whaleSightingsPendingListResponses);
    }
}
