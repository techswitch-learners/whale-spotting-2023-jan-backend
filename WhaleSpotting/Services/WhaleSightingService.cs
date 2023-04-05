using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Repositories;
using WhaleSpotting.Utilities;

namespace WhaleSpotting.Services;

public interface IWhaleSightingService
{
   public WhaleSighting GetById(int id);
   public void CreateSighting(WhaleSightingRequest whaleSightingRequest, string AuthHeader);
   public void ApproveSighting(int id);
   public void RejectId(int id);
   public List<WhaleSightingResponse> GetPendingSightings();
   public List<WhaleSightingResponse> ListApprovedSightings();
   public List<WhaleSightingResponse> Search(WhaleSightingSearchRequest whaleSightingSearchRequest);
}

public class WhaleSightingService : IWhaleSightingService
{
    private readonly IWhaleSightingRepo _whaleSighting;
    private readonly IUserRepo _users;
    private readonly ISpeciesRepo _species;

    public WhaleSightingService(IWhaleSightingRepo whaleSighting, IUserRepo users, ISpeciesRepo species)
    {
        _whaleSighting = whaleSighting;
        _users = users;
        _species = species;
    }

    public WhaleSighting GetById(int id)
    {
        return _whaleSighting.GetById(id);
    }

    public void CreateSighting(WhaleSightingRequest whaleSightingRequest, string authHeader)
    {
        string userName = AuthHelper.ExtractFromAuthHeader(authHeader).Username;
        User user = _users.GetByUsername(userName);
        WhaleSpecies species = _species.GetByName(whaleSightingRequest.WhaleSpecies);
        _whaleSighting.CreateSighting(whaleSightingRequest, user, species);
    }
    public void ApproveSighting(int id)
    {
        _whaleSighting.ApproveSighting(id);
    }
    public void RejectId(int id)
    {
        _whaleSighting.RejectId(id);
    }

    public List<WhaleSightingResponse> GetPendingSightings()
    {
        return _whaleSighting.GetPendingSightings();
    }

    public List<WhaleSightingResponse> ListApprovedSightings()
    {
        return _whaleSighting.ListApprovedSightings();
    }

    public List<WhaleSightingResponse> Search(WhaleSightingSearchRequest whaleSightingSearchRequest) {
        return _whaleSighting.Search(whaleSightingSearchRequest);
    }
}
