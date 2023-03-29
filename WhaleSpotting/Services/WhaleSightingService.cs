using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Services;

public interface IWhaleSightingService
{
   public WhaleSighting GetById(int id);
   public List<WhaleSighting> Search(WhaleSightingSearchRequest whaleSightingSearchRequest);
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

    public List<WhaleSighting> Search(WhaleSightingSearchRequest whaleSightingSearchRequest) {
        return _whaleSighting.Search(whaleSightingSearchRequest);
    }
}
