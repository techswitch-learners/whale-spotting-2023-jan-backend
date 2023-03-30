namespace WhaleSpotting.Models.Request;

public class WhaleSightingSearchRequest
{
    public string? WhaleSpecies { get; set; }
    public string? Colour { get; set; }
	public string? TailType { get; set; }
    public string? Size { get; set; }
    public float? MaxLatitude { get; set; }
    public float? MinLatitude { get; set; }
    public float? MaxLongitude { get; set; }
    public float? MinLongitude { get; set; }
}
