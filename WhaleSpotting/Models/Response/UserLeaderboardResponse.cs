 using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;
public class UserLeaderboardResponse
{
    public int Id { get; set; }

    public string UserName { get; set; }
    public int NumberOfWhaleSightings{get;set;}
    public int LikesGiven{get;set;}
    public int LikesReceived{get;set;}

    // List<WhaleSighting> WhaleSighting { get; set; }
    // List<Like> Likes{get;set;}

    public UserLeaderboardResponse(User user)
    {
        UserName=user.Username;
       NumberOfWhaleSightings=user.WhaleSighting.Count();
       LikesGiven=user.Likes.Count();
       LikesReceived=user.WhaleSighting.Sum(item=>item.Likes.Count()); //the sum of LikesCount in a list of WhaleSightings 
    }
}
 