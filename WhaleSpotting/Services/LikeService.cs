using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;
using WhaleSpotting.Utilities;

namespace WhaleSpotting.Services;

public interface ILikeService
{
    public void Create(LikeRequest newLikeRequest, string authHeader);
    public void Delete(LikeRequest newUnlike, string authHeader);
    public Like GetLikeById(int likeId);
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
    
    public void Delete(LikeRequest newUnlike, string authHeader)
    {
        _likes.Delete(newUnlike, authHeader);
    }

    public Like GetLikeById(int likeId)
    {
        return _likes.GetLikeById(likeId);
    }
}
