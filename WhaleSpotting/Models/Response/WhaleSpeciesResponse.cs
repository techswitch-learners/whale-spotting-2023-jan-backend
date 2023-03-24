using WhaleSpotting.Models.Database;

namespace WhaleSpotting.Models.Response;
public class WhaleSpeciesResponse
{

    public int Id {get;set;}
    public string Name {get;set;}
    public TailType TailType {get;set;}
    public TeethType TeethType {get;set;}
    public string ImageUrl {get;set;}
    public string Colour {get;set;}
    public string Location {get;set;}
    public string Diet {get;set;}
    public WhaleSize Size {get;set;}
    public WhaleSpeciesResponse (WhaleSpecies whaleSpecies) {

            Id = whaleSpecies.Id;
            Name = whaleSpecies.Name;
            TailType = whaleSpecies.TailType;
            TeethType = whaleSpecies.TeethType;
            ImageUrl = whaleSpecies.ImageUrl;
            Colour = whaleSpecies.Colour;
            Location = whaleSpecies.Location;
            Diet =whaleSpecies.Diet;
            Size = whaleSpecies.Size;
        }
}
  