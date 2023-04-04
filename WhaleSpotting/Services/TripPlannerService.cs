using WhaleSpotting.Repositories;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Services;
public interface ITripPlannerService
{
    public List<TripPlannerResponse> ListNearBySightings(float inputLat, float inputLon);
}
public class TripPlannerService : ITripPlannerService
{
    private readonly IWhaleSightingRepo _tripPlanner;
    public TripPlannerService(IWhaleSightingRepo tripPlanner)
    {
        _tripPlanner = tripPlanner;
    }
    public List<TripPlannerResponse> ListNearBySightings(float inputLat, float inputLon)
    {
        return _tripPlanner.ListNearBySightings(inputLat, inputLon);
    }
}