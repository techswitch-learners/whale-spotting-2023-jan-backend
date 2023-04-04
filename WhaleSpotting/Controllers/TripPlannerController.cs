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
    public ActionResult<List<TripPlannerResponse>> GetNearBySightings([FromQuery] TripPlannerRequest tripPlannerRequest)
    {
        try
        {
            var topFiveSightings = _tripPlannerService.ListNearBySightings(tripPlannerRequest.lat, tripPlannerRequest.lon);
            return topFiveSightings;
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound();
        }
    }
}
