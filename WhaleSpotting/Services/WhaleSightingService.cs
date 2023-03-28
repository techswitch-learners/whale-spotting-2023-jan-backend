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

    public WhaleSightingService(IWhaleSightingRepo whaleSighting, IUserRepo users)
    {
        _whaleSighting = whaleSighting;
        _users = users;
    }

    public WhaleSighting GetById(int id)
    {
        return _whaleSighting.GetById(id);
    }

    public void CreateSighting(WhaleSightingRequest whaleSightingRequest, string authHeader)
    {
        // var userName = AuthHelper.ExtractFromAuthHeader(authHeader).Username;
        // var ourUser = _users.GetByUsername(userName);
        var ourUser = _users.GetById(2);
        _whaleSighting.CreateSighting(whaleSightingRequest, ourUser);
    }
}
