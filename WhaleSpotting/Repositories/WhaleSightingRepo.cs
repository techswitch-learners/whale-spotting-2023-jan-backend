using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    public WhaleSighting GetById(int id);
    public void ApproveSighting(int id);
    public void RejectId(int id);
    public List<WhaleSightingResponse> GetPendingSightings();
    public List<WhaleSightingResponse> ListApprovedSightings();
    public List<TripPlannerResponse> ListNearBySightings(float inputLat, float inputLon);
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

    public List<TripPlannerResponse> ListNearBySightings(float inputLat, float inputLon)
    {
        try
        {
            List<TripPlannerResponse> SighingsList = context.WhaleSightings
             .Where(ws => (int)ws.ApprovalStatus == 1)
             .Include(ws => ws.WhaleSpecies)
             .Select(x => new TripPlannerResponse(x, inputLat, inputLon))
             .AsEnumerable()
             .OrderBy(x => x.Distance)
             .Take(5)
             .ToList();
            return (SighingsList);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No NearBy Sightings listed", ex);
        }
    }
}

