using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;
public class UserLeaderboardResponse
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public int NumberOfWhaleSightings { get; set; }

    public int LikesGiven { get; set; }
    
    public int LikesReceived { get; set; }

    public UserLeaderboardResponse(User user)
    {
        UserName = user.Username;
        NumberOfWhaleSightings = user.WhaleSighting
            .Where(ws => ws.ApprovalStatus == (ApprovalStatus)1)
            .Count();
        LikesGiven = user.Likes
            .Count();
        LikesReceived = user.WhaleSighting
            .Where(ws => ws.ApprovalStatus == (ApprovalStatus)1)
            .Sum(item => item.Likes
            .Count()); 
    }
}
