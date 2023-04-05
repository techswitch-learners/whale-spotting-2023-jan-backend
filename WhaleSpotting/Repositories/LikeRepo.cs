using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Utilities;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Repositories;

public interface ILikeRepo
{
    public void Create(LikeRequest newLikeRequest, int userId);
    public void Delete(LikeRequest newUnlike, string authHeader);
    public Like GetLikeById(int likeId);
}

public class LikeRepo : ILikeRepo
{
    private readonly WhaleSpottingDbContext context;
    private readonly IUserRepo _users;

    public LikeRepo(WhaleSpottingDbContext context, IUserRepo users)
    {
        this.context = context;
        this._users = users;
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

    public void Delete(LikeRequest newUnlike, string authHeader)
    {
        string username = AuthHelper.ExtractFromAuthHeader(authHeader).Username;
        var userId = _users.GetByUsername(username).Id;
        var targetLike = context.Likes.Where(i => i.UserId == userId && i.WhaleSightingId == newUnlike.WhaleSightingId)
                                .FirstOrDefault();

        var like = GetLikeById(targetLike.Id);
        if (like != null)
        {
            context.Likes.Remove(like);
            context.SaveChanges();
        }
        else
        {
            throw new ArgumentOutOfRangeException("No Like was found in the database");
        }
    }

    public Like GetLikeById(int likeId)
    {
        var like = context.Likes.FirstOrDefault(s => s.Id == likeId);
        if (like != null)
        {
            return like;
        }
        else
        {
            throw new ArgumentOutOfRangeException("No Like was found in the database");
        }
    }
}
