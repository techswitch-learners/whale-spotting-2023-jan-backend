using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using WhaleSpotting.Models.Response;


namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    public WhaleSighting GetById(int id);
    public void CreateSighting(WhaleSightingRequest whaleSightingRequest, User ourUser, WhaleSpecies whaleSpecies);
    public void ApproveSighting(int id);
    public void RejectId(int id);
    public List<WhaleSightingResponse> GetPendingSightings();
    public List<WhaleSightingResponse> ListApprovedSightings();
    public List<TripPlannerResponse> ListNearBySightings(float inputLat, float inputLon);
    List<WhaleSightingResponse> Search(WhaleSightingSearchRequest whaleSightingSearchRequest);
}

public class WhaleSightingRepo : IWhaleSightingRepo
{
    private readonly WhaleSpottingDbContext _context;
    public WhaleSightingRepo(WhaleSpottingDbContext context)
    {
        _context = context;
    }

    public WhaleSighting GetById(int id)
    {
        try
        {
            return _context.WhaleSightings
                .Where(ws => ws.Id == id)
                .Include(ws => ws.User)
                .Include(ws => ws.WhaleSpecies)
                .Include(ws => ws.Likes)
                    .ThenInclude(wsl => wsl.User)
                .FirstOrDefault();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No sighting with id {id} found in the database", ex);
        }
    }

    async public void CreateSighting(WhaleSightingRequest whaleSightingRequest, User ourUser, WhaleSpecies whaleSpecies)
    {
        _context.Users.Attach(ourUser);
        _context.WhaleSpecies.Attach(whaleSpecies);
        _context.WhaleSightings.Add(new WhaleSighting
        {
            DateOfSighting = whaleSightingRequest.DateOfSighting,
            LocationLatitude = whaleSightingRequest.LocationLatitude,
            LocationLongitude = whaleSightingRequest.LocationLongitude,
            PhotoImageURL = whaleSightingRequest.PhotoImageURL,
            NumberOfWhales = whaleSightingRequest.NumberOfWhales,
            Description = whaleSightingRequest.Description,
            ApprovalStatus = ApprovalStatus.Pending,
            WhaleSpecies = whaleSpecies,
            User = ourUser,
        });
        _context.SaveChanges();
    }

    public void ApproveSighting(int id)
    {
        try
        {
            var selectedSighting = _context.WhaleSightings.FirstOrDefault(w => w.Id == id);
            selectedSighting.ApprovalStatus = ApprovalStatus.Approved;
            _context.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No sighting with id {id} found in the database", ex);
        }
    }

    public void RejectId(int id)
    {
        var rejectSighting = _context.WhaleSightings
            .FirstOrDefault(ws => ws.Id == id);

        rejectSighting.ApprovalStatus = ApprovalStatus.Deleted;
        _context.WhaleSightings.Update(rejectSighting);
        _context.SaveChanges();
    }

    public List<WhaleSightingResponse> GetPendingSightings()
    {
        return _context.WhaleSightings.Where(ws => (int)ws.ApprovalStatus == 0)
            .Include(ws => ws.User)
            .Include(ws => ws.WhaleSpecies)
            .Include(ws => ws.Likes)
                .ThenInclude(wsl => wsl.User)
            .Select(x => new WhaleSightingResponse(x))
            .AsEnumerable()
            .OrderByDescending(ws => ws.DateOfSighting)
            .ToList();
    }

    public List<WhaleSightingResponse> ListApprovedSightings()
    {
        try
        {
            return _context.WhaleSightings.Where(ws => (int)ws.ApprovalStatus == 1)
            .Include(ws => ws.User)
            .Include(ws => ws.WhaleSpecies)
            .Include(ws => ws.Likes)
                .ThenInclude(wsl => wsl.User)
            .Select(x => new WhaleSightingResponse(x))
            .AsEnumerable()
            .OrderByDescending(ws => ws.DateOfSighting)
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
            List<TripPlannerResponse> SighingsList = _context.WhaleSightings
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

    public List<WhaleSightingResponse> Search(WhaleSightingSearchRequest whaleSightingSearchRequest)
    {
        try
        {
            var query = _context.WhaleSightings.Where(ws => (int)ws.ApprovalStatus == 1)
                .Include(ws => ws.User)
                .Include(ws => ws.WhaleSpecies)
                .Include(ws => ws.Likes)
                    .ThenInclude(wsl => wsl.User);

            var filteredQuery = query
                    .Where(ws => string.IsNullOrEmpty(whaleSightingSearchRequest.Name)
                        || ws.WhaleSpecies.Name.ToLower() == whaleSightingSearchRequest.Name.ToLower())
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

            return filteredQuery.Select(ws => new WhaleSightingResponse(ws)).AsEnumerable().OrderByDescending(ws => ws.DateOfSighting).ToList();
        }
        catch (SystemException ex)
        {
            throw new SystemException(ex.Message);
        }
    }
}

