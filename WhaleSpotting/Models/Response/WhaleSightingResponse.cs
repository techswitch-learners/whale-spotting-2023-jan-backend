using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;

public class WhaleSightingResponse
{
    private readonly WhaleSighting _whaleSighting;

    public WhaleSightingResponse(WhaleSighting whaleSighting)
    {
        _whaleSighting = whaleSighting;
    }
    public int Id => _whaleSighting.Id;
    public DateTime DateOfSighting => _whaleSighting.DateOfSighting;
    public float LocationLatitude => _whaleSighting.LocationLatitude;
    public float LocationLongitude => _whaleSighting.LocationLongitude;
    public string PhotoImageURL => _whaleSighting.PhotoImageURL;
    public string Description => _whaleSighting.Description;
    public int NumberOfWhales => _whaleSighting.NumberOfWhales;
    public ApprovalStatus ApprovalStatus => _whaleSighting.ApprovalStatus;
    public WhaleSpeciesResponse WhaleSpecies => new WhaleSpeciesResponse(_whaleSighting.WhaleSpecies) ;
    public UserResponse User => new UserResponse(_whaleSighting.User);
}
