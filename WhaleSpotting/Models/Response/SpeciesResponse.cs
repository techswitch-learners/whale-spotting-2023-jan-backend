using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Models.Response;

public class SpeciesResponse
{
    private readonly WhaleSpecies _species;

    public SpeciesResponse(WhaleSpecies species)
    {
        _species = species;
    }

    public int Id => _species.Id;
    public string ImageUrl => _species.ImageUrl;
    public string Name => _species.Name;
    public TailType TailType => _species.TailType;
    public TeethType TeethType => _species.TeethType;
    public WhaleSize Size => _species.Size;
    public string Colour => _species.Colour;
    public string Location => _species.Location;
    public string Diet => _species.Diet;
}
