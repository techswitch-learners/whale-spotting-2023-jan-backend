namespace WhaleSpotting.Models.Request;

public class CreateUserRequest
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? UserBio { get; set; }
    public int UserType { get; set; }
    public CreateUserRequest(string userName, string password, string profileImageUrl, string userBio)
    {
        Username = userName;
        Password = password;
        ProfileImageUrl = profileImageUrl;
        UserBio = userBio;
        UserType = 1;
    }
}
