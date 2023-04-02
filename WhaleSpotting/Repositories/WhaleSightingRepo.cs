using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    public WhaleSighting GetById(int id);
    public void CreateSighting(WhaleSightingRequest whaleSightingRequest, User ourUser, WhaleSpecies whaleSpecies);
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
                .FirstOrDefault();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No sightning with id {id} found in the database", ex);
        }
    }

    async public void CreateSighting(WhaleSightingRequest whaleSightingRequest, User user, WhaleSpecies whaleSpecies)
    {
        _context.Users.Attach(user);
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
            User = user,
        });
        _context.SaveChanges();
    }
}
