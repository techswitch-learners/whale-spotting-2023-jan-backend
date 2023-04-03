 using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;
public class UserLeaderboardResponse
{
    public int Id { get; set; }

    public string UserName { get; set; }
    public int WhaleSightingId{get;set;}
    public int LikeId{get;set;}

    // List<WhaleSighting> WhaleSighting { get; set; }
    // List<Like> Likes{get;set;}

    public UserLeaderboardResponse(string username,int whalesightingid,int likeid)
    {
        UserName=username;
        WhaleSightingId=whalesightingid;
        LikeId=likeid;
    }
}
 