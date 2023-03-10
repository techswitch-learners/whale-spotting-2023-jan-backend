using Microsoft.AspNetCore.Mvc;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Test()
    {
        return Ok("The Whale Spotting API is running correctly!");
    }
}