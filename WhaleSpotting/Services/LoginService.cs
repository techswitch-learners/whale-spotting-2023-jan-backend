using WhaleSpotting.Repositories;

namespace WhaleSpotting.Services;

public interface ILoginService
{
    public bool IsValidLogin(string username, string password);
    public bool IsAdmin(string username);
}

public class LoginService : ILoginService
{
    private readonly IUserRepo _users;

    public LoginService(IUserRepo users)
    {
        _users = users;
    }

    public bool IsValidLogin(string username, string password)
    {
        try
        {
            var user = _users.GetByUsername(username);
            return user.IsPasswordValid(password);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsAdmin(string username)
    {
        try
        {
            var user = _users.GetByUsername(username);
            return user.UserType == (UserType)1;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
