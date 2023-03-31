using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Services;
using WhaleSpotting.Utilities;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet("")]
    public IActionResult IsValidLogin([FromHeader] string authorization)
    {
        (string Username, string Password) details;
         
        try
        {
            details = AuthHelper.ExtractFromAuthHeader(authorization);
        }
        catch (Exception)
        {
            return Unauthorized(
                "Authorization header was not valid. Ensure you are using basic auth, and have correctly base64-encoded your username and password.");
        }

        if (_loginService.IsValidLogin(details.Username, details.Password))
        {
            return Ok();
        }

        return Unauthorized("Invalid login details.");
    }

    [HttpGet("admin")]
    public IActionResult IsAdmin([FromHeader] string authorization)
    {
        (string Username, string password) details;
         
        try
        {
            details = AuthHelper.ExtractFromAuthHeader(authorization);
        }
        catch (Exception)
        {
            return Unauthorized(
                "Authorization header was not valid. Ensure you are using basic auth, and have correctly base64-encoded your username and password.");
        }

        if (_loginService.IsAdmin(details.Username))
        {
            return Ok();
        }

        return Unauthorized("User is not admin");
    }
}
