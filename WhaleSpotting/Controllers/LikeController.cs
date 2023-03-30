using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Services;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("likes")]
public class LikeController : ControllerBase
{
    private readonly ILikeService _likesService;

    public LikeController(ILikeService likesService)
    {
        _likesService = likesService;
    }
    
    [HttpPost("create")]
    public IActionResult CreateLike([FromBody] LikeRequest newLike, [FromHeader(Name = "Authorization")] string authHeader)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            _likesService.Create(newLike, authHeader);
            return Ok($"User liked whalesighting Id: {newLike.WhaleSightingId}.");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete/{likeId:int}")]
    public IActionResult DeleteLike([FromRoute] int likeId)
    {
        try
        {
            _likesService.Delete(likeId);
            return Ok($"Like deleted");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
        }
    }

    [HttpGet("{likeId:int}")]
    public IActionResult GetLikeById([FromRoute] int likeId)
    {
        try
        {
            var newLike = _likesService.GetLikeById(likeId);
            return Ok(newLike);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Error retrieving Like with id: " + likeId);
        }
    }
}
