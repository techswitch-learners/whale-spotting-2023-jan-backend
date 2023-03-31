using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;
public class LikeResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int WhaleSightingId { get; set; }
    public string Username { get; set; } = null!;

    public LikeResponse(Like like)
    {
        Id = like.Id;
        Date = like.Date;
        WhaleSightingId = like.WhaleSightingId;

        Username = like.User.Username;
    }
}
