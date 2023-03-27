
namespace WhaleSpotting.Models.Request;
public class SpeciesSearchRequest
{
    public TailType? TailType { get; set; }
    public WhaleSize? Size { get; set; }
    public string? Colour { get; set; }
}
