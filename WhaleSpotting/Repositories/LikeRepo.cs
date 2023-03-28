using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Repositories;

public interface ILikeRepo
{
    public void Create(LikeRequest newLikeRequest, int userId);
    public void Delete(int likeId);
}

public class LikeRepo : ILikeRepo
{
    private readonly WhaleSpottingDbContext context;

    public LikeRepo(WhaleSpottingDbContext context)
    {
        this.context = context;
    }
    
    public void Create(LikeRequest newLikeRequest, int userId)
    {
        context.Likes.Add(new Like
        {
            Date = DateTime.Now.ToUniversalTime(),
            WhaleSightingId = newLikeRequest.WhaleSightingId,
            UserId = userId,
        });
        context.SaveChanges();
    }
    
    public void Delete(int likeId)
    {
        var like = new Like { Id = likeId };
        context.Likes.Remove(like);
        context.SaveChanges();
    }
}
