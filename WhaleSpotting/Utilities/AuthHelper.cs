using System.Text;
using WhaleSpotting.Services;

namespace WhaleSpotting.Utilities;

public static class AuthHelper
{
    public static (string Username, string Password) ExtractFromAuthHeader(string authHeader)
    {
        if (authHeader[.."Basic ".Length] != "Basic ")
        {
            throw new ArgumentOutOfRangeException(nameof(authHeader), authHeader, "Authorization header is not using basic authentication");
        }

        string encodedUsernameAndPassword = authHeader["Basic ".Length..];
        string usernameAndPassword = Base64Decode(encodedUsernameAndPassword);
        string[] splitUsernameAndPassword = usernameAndPassword.Split(':');

        if (splitUsernameAndPassword.Length != 2)
        {
            throw new ArgumentOutOfRangeException(nameof(authHeader),
                "Base64 encoded string cannot be converted correctly into a username and password");
        }

        return (splitUsernameAndPassword[0], splitUsernameAndPassword[1]);
    }

    static string Base64Decode(string encoded)
    {
        byte[] decodedBytes = Convert.FromBase64String(encoded);
        return Encoding.UTF8.GetString(decodedBytes);
    }

    public static bool LoginChecker(string authorization, ILoginService loginService)
    {
        (string Username, string Password) details;

        try
        {
            details = AuthHelper.ExtractFromAuthHeader(authorization);
            return (loginService.IsValidLogin(details.Username.ToLower(), details.Password) && loginService.IsAdmin(details.Username.ToLower()));
        }
        catch (Exception)
        {
            return false;
        }
    }
};