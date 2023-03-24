namespace WhaleSpotting.Models.Request;

public class WhaleSightingRequest
{
    public DateTime DateOfSighting { get; set; }
    public float LocationLatitude { get; set; }
    public float LocationLongitude { get; set; }
    public string PhotoImageURL { get; set; }
    public int NumberOfWhales { get; set; }
    public string Description { get; set; }
    public string WhaleSpecies { get; set; }
}
