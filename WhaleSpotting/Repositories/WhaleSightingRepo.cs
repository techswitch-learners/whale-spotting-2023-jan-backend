using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    public WhaleSighting GetById(int id);
    public List<WhaleSightingResponse> GetPendingSightings();
    public List<WhaleSightingResponse> ListApprovedSightings();
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
}
