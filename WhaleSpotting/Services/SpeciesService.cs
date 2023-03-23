using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Services;

public interface ISpeciesService
{
    IEnumerable<SpeciesResponse> Search(SpeciesSearchRequest search);
    int Count(List<SpeciesResponse> species);

}

public class SpeciesService : ISpeciesService
{
    private readonly ISpeciesRepo _species;

    public SpeciesService(ISpeciesRepo species)
    {
        _species = species;
    }

    public IEnumerable<SpeciesResponse> Search(SpeciesSearchRequest search)
    {
        return _species.Search(search);
    }

    public int Count(List<SpeciesResponse> species)
    {
        return species
            .Count();
    }
}
