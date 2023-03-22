using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Repositories;

public interface ILikeRepo
{
    public void Create(LikeRequest newLikeRequest);
}

public class LikeRepo : ILikeRepo
{
    private readonly WhaleSpottingDbContext context;

    public LikeRepo(WhaleSpottingDbContext context)
    {
        this.context = context;
    }
    
    public void Create(LikeRequest newLikeRequest)
    {
        var insertResult = context.Likes.Add(new Like
        {
            Date = DateTime.Now.ToUniversalTime(),
            WhaleSightingId = newLikeRequest.WhaleSightingId,
            UserId = newLikeRequest.UserId,
        });
        context.SaveChanges();
    }
}