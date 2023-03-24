
using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;

public class SpeciesResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public TailType TailType { get; set; }
    public TeethType TeethType { get; set; }
    public WhaleSize Size { get; set; }
    public string Colour { get; set; }
    public string Location { get; set; }
    public string Diet { get; set; }
    public SpeciesResponse (WhaleSpecies species)
    {
        Id = species.Id;
        ImageUrl = species.ImageUrl;
        Name = species.Name;
        TailType = species.TailType;
        TeethType = species.TeethType;
        Size = species.Size;
        Colour = species.Colour;
        Location = species.Location;
        Diet = species.Diet;
    }
}