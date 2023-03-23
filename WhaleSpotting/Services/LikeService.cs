using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;
using WhaleSpotting.Utilities;

namespace WhaleSpotting.Services;

public interface ILikeService
{
    public void Create(LikeRequest newLikeRequest, string authHeader);
}

public class LikeService : ILikeService
{
    private readonly ILikeRepo _likes;
    private readonly IUserRepo _users;

    public LikeService(ILikeRepo likes, IUserRepo users)
    {
        _likes = likes;
        _users = users;
    }
    
    public void Create(LikeRequest newLikeRequest, string authHeader)
    {
        var userName = AuthHelper.ExtractFromAuthHeader(authHeader).Username;
        var userId = _users.GetByUsername(userName).Id;

        _likes.Create(newLikeRequest, userId);
    }
}
