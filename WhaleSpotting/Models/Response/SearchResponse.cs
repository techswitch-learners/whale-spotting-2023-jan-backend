using System.Collections.Generic;
using System.Linq;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Models.Response;
public class SearchResponse
{
    // private readonly string _path;
    public IEnumerable<SpeciesResponse> Items { get; }

    public SearchResponse(IEnumerable<SpeciesResponse> items)
    {
        Items = items;
        // _path = path;
    }

    public static SearchResponse Create(IEnumerable<SpeciesResponse> species)
    {
        var speciesModels = species.ToList();
        return new SearchResponse(speciesModels);
    }
}
