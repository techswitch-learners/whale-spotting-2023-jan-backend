using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;

public class TripPlannerResponse
{
    public int Id { get; set; }
    public DateTime DateOfSighting { get; set; }
    public float LocationLatitude { get; set; }
    public float LocationLongitude { get; set; }
    public string PhotoImageURL { get; set; }
    public float Distance { get; set; }
    public int NumberOfWhales { get; set; }
    public WhaleSpeciesResponse WhaleSpecies { get; set; }

    public TripPlannerResponse(WhaleSighting whaleSighting, float inputLat, float inputLon)
    {
        Id = whaleSighting.Id;
        DateOfSighting = whaleSighting.DateOfSighting;
        LocationLatitude = whaleSighting.LocationLatitude;
        LocationLongitude = whaleSighting.LocationLongitude;
        PhotoImageURL = whaleSighting.PhotoImageURL;
        NumberOfWhales = whaleSighting.NumberOfWhales;
        WhaleSpecies = new WhaleSpeciesResponse(whaleSighting.WhaleSpecies);
        Distance = ((float)(Math.Acos(Math.Sin(inputLat) * Math.Sin(LocationLatitude) + Math.Cos(inputLat) * Math.Cos(LocationLatitude) * Math.Cos(LocationLongitude - inputLon)) * 6371));
    }
}
