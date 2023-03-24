using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;

public class WhaleSightingResponse
{
    public int Id { get; set; }
    public DateTime DateOfSighting { get; set; }
    public float LocationLatitude { get; set; }
    public float LocationLongitude { get; set; }
    public string PhotoImageURL { get; set; }
    public string Description { get; set; }
    public int NumberOfWhales { get; set; }
    public ApprovalStatus ApprovalStatus { get; set; }
    public WhaleSpeciesResponse WhaleSpecies { get; set; }
    public UserResponse User { get; set; }  
      
    public WhaleSightingResponse(WhaleSighting whaleSighting)
    {
        Id = whaleSighting.Id;
        DateOfSighting = whaleSighting.DateOfSighting;
        LocationLatitude = whaleSighting.LocationLatitude;
        LocationLongitude= whaleSighting.LocationLongitude;
        PhotoImageURL = whaleSighting.PhotoImageURL;
        Description = whaleSighting.Description;
        NumberOfWhales = whaleSighting.NumberOfWhales;
        ApprovalStatus = whaleSighting.ApprovalStatus;
        WhaleSpecies = new WhaleSpeciesResponse(whaleSighting.WhaleSpecies) ;
        User = new UserResponse(whaleSighting.User);
    }
}
