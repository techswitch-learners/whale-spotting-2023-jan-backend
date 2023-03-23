using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;
public class WhaleSpeciesResponse
{
    private readonly WhaleSpecies _whaleSpecies;

    public WhaleSpeciesResponse(WhaleSpecies whaleSpecies)
    {
        _whaleSpecies = whaleSpecies;
    }
    public int Id => _whaleSpecies.Id;
    public string Name => _whaleSpecies.Name;
    public TailType TailType => _whaleSpecies.TailType;
    public TeethType TeethType => _whaleSpecies.TeethType;
    public string ImageUrl => _whaleSpecies.ImageUrl;
    public string Colour => _whaleSpecies.Colour;
    public string Location => _whaleSpecies.Location;
    public string Diet => _whaleSpecies.Diet;
    public WhaleSize Size => _whaleSpecies.Size;
}
