using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Services;

public interface IWhaleSightingService
{
   public WhaleSighting GetById(int id);
   public void RejectId(int id);
   public List<WhaleSightingResponse> ListApprovedSightings();
}

public class WhaleSightingService : IWhaleSightingService
{
    private readonly IWhaleSightingRepo _whaleSighting;

    public WhaleSightingService(IWhaleSightingRepo whaleSighting)
    {
        _whaleSighting = whaleSighting;
    }

    public WhaleSighting GetById(int id)
    {
        return _whaleSighting.GetById(id);
    }

    public void RejectId(int id)
    {
        _whaleSighting.RejectId(id);
    }

    public List<WhaleSightingResponse> ListApprovedSightings()
    {
        return _whaleSighting.ListApprovedSightings();
    }
}
