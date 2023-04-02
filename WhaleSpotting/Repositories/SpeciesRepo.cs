
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Repositories;

public interface ISpeciesRepo
{
    List<SpeciesResponse> Search(SpeciesSearchRequest search);
    public WhaleSpecies GetByName(string name);
}
public class SpeciesRepo : ISpeciesRepo
{
    private readonly WhaleSpottingDbContext _context;

    public SpeciesRepo(WhaleSpottingDbContext context)
    {
        _context = context;
    }
    public List<SpeciesResponse> Search(SpeciesSearchRequest search)
    {
        try
        {
            return _context.WhaleSpecies
                .Where(s => search.TailType == null ||
                                (
                                    s.TailType == search.TailType
                                ))
                .Where(s => search.Size == null ||
                                    (
                                        s.Size == search.Size
                                    ))
                .Where(s => search.Colour == null ||
                                    (
                                        s.Colour == search.Colour
                                    ))
                .Select(x => new SpeciesResponse(x))
                .AsEnumerable()
                .OrderBy(s => s.Id).ToList();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No species was found in the database", ex);
        }
    }

    public WhaleSpecies GetByName(string name)
    {
        try
        {
            return _context.WhaleSpecies.Single(species => species.Name == name);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No species with name {name} found in the database", ex);
        }
    }
}
