
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Repositories;

public interface ISpeciesRepo
{
    IEnumerable<SpeciesResponse> Search(SpeciesSearchRequest search);
}
public class SpeciesRepo : ISpeciesRepo
{
    private readonly WhaleSpottingDbContext _context;

    public SpeciesRepo(WhaleSpottingDbContext context)
    {
        _context = context;
    }
    public IEnumerable<SpeciesResponse> Search(SpeciesSearchRequest search)
    {
        try
        {
            return _context.WhaleSpecies
                //enum value is assigned from 0 onwards if no other value is assigned
                .Where(p => search.TailType == null ||
                                (
                                    p.TailType == search.TailType
                                ))
                .Where(p => search.Size == null ||
                                    (
                                        p.Size == search.Size
                                    ))
                .Where(p => search.Colour == null ||
                                    (
                                        p.Colour == search.Colour
                                    ))
                .Select(x => new SpeciesResponse(x))
                .AsEnumerable()
                .OrderBy(p => p.Id);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No whale was found in the database", ex);
        }
    }
}
