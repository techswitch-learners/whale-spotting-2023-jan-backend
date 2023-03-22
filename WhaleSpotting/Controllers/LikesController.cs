using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Services;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("likes")]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likesService;

    public LikesController(ILikeService likesService)
    {
        _likesService = likesService;
    }
    [HttpPost("createLike")]
    public IActionResult CreateLike([FromBody] LikeRequest newLike)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            _likesService.Create(newLike);
            return Ok($"User Id: {newLike.UserId} liked whalesighting Id: {newLike.WhaleSightingId}.");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }   
    }
}