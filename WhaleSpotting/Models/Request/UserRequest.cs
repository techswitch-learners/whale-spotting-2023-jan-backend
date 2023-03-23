namespace WhaleSpotting.Models.Request;

public class UserRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? UserBio { get; set; }
}