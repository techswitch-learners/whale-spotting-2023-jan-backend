using Microsoft.AspNetCore.Mvc;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Services;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Controllers;

[ApiController]
[Route("species")]
public class SpeciesController : ControllerBase
{
    private readonly ISpeciesService _speciesService;

    public SpeciesController(ISpeciesService speciesService)
    {
        _speciesService = speciesService;
    }

    [HttpGet("")]
    public ActionResult<List<WhaleSpeciesResponse>> Search([FromQuery] SpeciesSearchRequest speciesRequest)
    {
        return _speciesService.Search(speciesRequest);
    }
}
