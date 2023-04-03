using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using Microsoft.EntityFrameworkCore;
using WhaleSpotting.Models.Response;

namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    public WhaleSighting GetById(int id);
    public void ApproveSighting(int id);
    public void RejectId(int id);
    public List<WhaleSightingResponse> GetPendingSightings();
    public List<WhaleSightingResponse> ListApprovedSightings();
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
            throw new ArgumentOutOfRangeException($"No sighting with id {id} found in the database", ex);
        }
    }

    public void ApproveSighting(int id)
    {
        try
        {
            var selectedSighting = context.WhaleSightings.FirstOrDefault(w => w.Id == id);
            selectedSighting.ApprovalStatus = (ApprovalStatus)1;
            context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No sighting with id {id} found in the database", ex);
        }
    }
    
    public void RejectId(int id)
    {
        var rejectSighting = context.WhaleSightings
            .FirstOrDefault(ws => ws.Id == id);

        rejectSighting.ApprovalStatus = ApprovalStatus.Deleted;
        context.WhaleSightings.Update(rejectSighting);
        context.SaveChanges();
    }

    public List<WhaleSightingResponse> GetPendingSightings()
    {
        return context.WhaleSightings
            .Where(ws => ws.ApprovalStatus == ApprovalStatus.Pending)
            .Include(ws => ws.User)
            .Include(ws => ws.WhaleSpecies)
            .Select(ws => new WhaleSightingResponse(ws))
            .ToList();
    }

    public List<WhaleSightingResponse> ListApprovedSightings()
    {
        try
        {
            return context.WhaleSightings.Where(ws => (int)ws.ApprovalStatus == 1)
            .Include(ws => ws.User)
            .Include(ws => ws.WhaleSpecies)
            .Select(x => new WhaleSightingResponse(x))
            .AsEnumerable()
            .OrderBy(ws => ws.Id)
            .ToList();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No approved whale sightings in the database", ex);
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
                    .Where(ws => !whaleSightingSearchRequest.MaxLongitude.HasValue 
                        || ws.LocationLongitude <= whaleSightingSearchRequest.MaxLongitude)
                    .Where(ws => !whaleSightingSearchRequest.MinLongitude.HasValue 
                        || ws.LocationLongitude >= whaleSightingSearchRequest.MinLongitude)
                    .Where(ws => !whaleSightingSearchRequest.MaxLatitude.HasValue
                        || ws.LocationLatitude <= whaleSightingSearchRequest.MaxLatitude)
                    .Where(ws => !whaleSightingSearchRequest.MinLatitude.HasValue 
                        || ws.LocationLatitude >= whaleSightingSearchRequest.MinLatitude)
                    .Where(ws => whaleSightingSearchRequest.TailType == null
                        || ws.WhaleSpecies.TailType == whaleSightingSearchRequest.TailType)
                    .Where(ws => whaleSightingSearchRequest.Size == null
                        || ws.WhaleSpecies.Size == whaleSightingSearchRequest.Size);
            
            return filteredQuery.Select(ws => new WhaleSightingResponse(ws)).ToList();
        }
        catch (SystemException ex)
        {
            throw new SystemException(ex.Message);
        }
    }
}

