using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    public WhaleSighting GetById(int id);
    List<WhaleSighting> Search(WhaleSightingSearchRequest whaleSightingSearchRequest);
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

    public List<WhaleSighting> Search(WhaleSightingSearchRequest whaleSightingSearchRequest) {
        try
        {
            var query = context.WhaleSightings
                .Include(ws => ws.User)
                .Include(ws => ws.WhaleSpecies)
                .ToList();

                if (!string.IsNullOrEmpty(whaleSightingSearchRequest.WhaleSpecies)) query = query.Where(ws => ws.WhaleSpecies.Name.ToLower() == whaleSightingSearchRequest.WhaleSpecies.ToLower()).ToList();
                
                if (!string.IsNullOrEmpty(whaleSightingSearchRequest.Colour)) query = query.Where(ws => ws.WhaleSpecies.Colour.ToLower() == whaleSightingSearchRequest.Colour.ToLower()).ToList();

                if (whaleSightingSearchRequest.TailType != null) query = query.Where(ws => ws.WhaleSpecies.TailType == whaleSightingSearchRequest.TailType).ToList();

                if (whaleSightingSearchRequest.Size != null) query = query.Where(ws => ws.WhaleSpecies.Size == whaleSightingSearchRequest.Size).ToList();
            
                if (whaleSightingSearchRequest.MaxLongitude != null) query = query.Where(ws => ws.LocationLongitude <= whaleSightingSearchRequest.MaxLongitude).ToList();

                if (whaleSightingSearchRequest.MinLongitude != null) query = query.Where(ws => ws.LocationLongitude >= whaleSightingSearchRequest.MinLongitude).ToList();

                if (whaleSightingSearchRequest.MaxLatitude != null) query = query.Where(ws => ws.LocationLatitude <= whaleSightingSearchRequest.MaxLatitude).ToList();

                if (whaleSightingSearchRequest.MinLatitude != null) query = query.Where(ws => ws.LocationLatitude >= whaleSightingSearchRequest.MinLatitude).ToList();
            
            return query.ToList();
        }
        catch (SystemException ex)
        {
            throw new SystemException(ex.Message);
        }
    }
}
