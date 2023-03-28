using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Services;

public interface IWhaleSightingService
{
   public WhaleSighting GetById(int id);
   public List<WhaleSighting> GetPendingSightings();
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

    public List<WhaleSighting> GetPendingSightings()
    {
        return _whaleSighting.GetPendingSightings();
    }
}
