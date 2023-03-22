using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Services;

public interface ILikeService
{
    public void Create(LikeRequest newLikeRequest);
}

public class LikeService : ILikeService
{
    private readonly ILikeRepo _likes;

    public LikeService(ILikeRepo likes)
    {
        _likes = likes;
    }
    
    public void Create(LikeRequest newLikeRequest)
    {
        _likes.Create(newLikeRequest);
    }
}