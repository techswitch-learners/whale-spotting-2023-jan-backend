using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;
using WhaleSpotting.Utilities;

namespace WhaleSpotting.Services;

public interface IWhaleSightingService
{
   public WhaleSighting GetById(int id);
   public void CreateSighting(WhaleSightingRequest whaleSightingRequest, string AuthHeader);
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
}
