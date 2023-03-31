using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Services;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("plan-trip")]
public class TripPlannerController : ControllerBase
{
    private readonly ITripPlannerService _tripPlannerService;

    public TripPlannerController(ITripPlannerService tripPlannerService)
    {
        _tripPlannerService = tripPlannerService;
    }

    [HttpGet("")]
    public ActionResult<List<TripPlannerResponse>> GetNearBySightings([FromQuery] TripPlannerReuquest input)
    {
        try
        {
            var response = _tripPlannerService.ListNearBySightings(input.lat, input.lon);
            return response;
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }
}
