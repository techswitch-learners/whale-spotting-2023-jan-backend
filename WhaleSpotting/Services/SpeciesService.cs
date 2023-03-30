
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Services;

public interface ISpeciesService
{
    List<WhaleSpeciesResponse> Search(SpeciesSearchRequest search);
    List<string> GetSpeciesList();
}

public class SpeciesService : ISpeciesService
{
    private readonly ISpeciesRepo _species;

    public SpeciesService(ISpeciesRepo species)
    {
        _species = species;
    }

    public List<WhaleSpeciesResponse> Search(SpeciesSearchRequest search)
    {
        return _species.Search(search);
    }

    public List<string> GetSpeciesList()
    {
        return _species.GetSpeciesList();
    }  
}
