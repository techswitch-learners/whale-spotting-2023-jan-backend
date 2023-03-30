using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    public WhaleSighting GetById(int id);
    List<WhaleSightingResponse> Search(WhaleSightingSearchRequest whaleSightingSearchRequest);
}

public class WhaleSightingRepo : IWhaleSightingRepo
{
    private readonly WhaleSpottingDbContext context;
    public WhaleSightingRepo(WhaleSpottingDbContext context)
    {
        this.context = context;
    }

    public WhaleSighting GetById(int id)
    {
        try
        {
            return context.WhaleSightings
                .Where(ws => ws.Id == id)
                .Include(ws => ws.User)
                .Include(ws => ws.WhaleSpecies)
                .FirstOrDefault();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No sightning with id {id} found in the database", ex);
        }
    }

    public List<WhaleSightingResponse> Search(WhaleSightingSearchRequest whaleSightingSearchRequest) {
        try
        {
            var query = context.WhaleSightings
                .Include(ws => ws.User)
                .Include(ws => ws.WhaleSpecies);

            var filteredQuery = query
                    .Where(ws => string.IsNullOrEmpty(whaleSightingSearchRequest.WhaleSpecies) 
                        || ws.WhaleSpecies.Name.ToLower() == whaleSightingSearchRequest.WhaleSpecies.ToLower())
                    .Where(ws => string.IsNullOrEmpty(whaleSightingSearchRequest.Colour) 
                        || ws.WhaleSpecies.Colour.ToLower() == whaleSightingSearchRequest.Colour.ToLower())
                    .Where(ws => (!whaleSightingSearchRequest.MaxLongitude.HasValue) 
                        || (ws.LocationLongitude <= whaleSightingSearchRequest.MaxLongitude))
                    .Where(ws => (!whaleSightingSearchRequest.MinLongitude.HasValue) 
                        || (ws.LocationLongitude >= whaleSightingSearchRequest.MinLongitude))
                    .Where(ws => (!whaleSightingSearchRequest.MaxLatitude.HasValue) 
                        || (ws.LocationLatitude <= whaleSightingSearchRequest.MaxLatitude))
                    .Where(ws => (!whaleSightingSearchRequest.MinLatitude.HasValue) 
                        || (ws.LocationLatitude >= whaleSightingSearchRequest.MinLatitude))
                    .Where(ws => string.IsNullOrEmpty(whaleSightingSearchRequest.TailType) 
                        || ws.WhaleSpecies.TailType == Enum.Parse<TailType>(whaleSightingSearchRequest.TailType))
                    .Where(ws => string.IsNullOrEmpty(whaleSightingSearchRequest.Size) 
                        || ws.WhaleSpecies.Size == Enum.Parse<WhaleSize>(whaleSightingSearchRequest.Size));
            
            return filteredQuery.Select(ws => new WhaleSightingResponse(ws)).ToList();
        }
        catch (SystemException ex)
        {
            throw new SystemException(ex.Message);
        }
    }
}
