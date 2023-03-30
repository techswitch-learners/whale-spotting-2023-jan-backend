
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Repositories;

public interface ISpeciesRepo
{
    List<WhaleSpeciesResponse> Search(SpeciesSearchRequest search);
    List<string> GetSpeciesList();
}
public class SpeciesRepo : ISpeciesRepo
{
    private readonly WhaleSpottingDbContext _context;

    public SpeciesRepo(WhaleSpottingDbContext context)
    {
        _context = context;
    }
    public List<WhaleSpeciesResponse> Search(SpeciesSearchRequest search)
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
                .Select(x => new WhaleSpeciesResponse(x))
                .AsEnumerable()
                .OrderBy(s => s.Id).ToList();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No species was found in the database", ex);
        }
    }

    public List<string> GetSpeciesList()
    {
        try
        {
            return _context.WhaleSpecies
                .Select(x => x.Name)
                .ToList();
        }
        catch (SystemException ex)
        {
            throw new SystemException(ex.Message);
        }
    }
}
