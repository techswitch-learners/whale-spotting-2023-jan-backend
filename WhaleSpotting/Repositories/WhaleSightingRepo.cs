using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace WhaleSpotting.Repositories;

public interface IWhaleSightingRepo
{
    WhaleSighting GetById(int id);
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
                .Where(u => u.Id == id)
                .Include(u => u.User)
                .Include(u => u.WhaleSpecies)
                .FirstOrDefault();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No user with id {id} found in the database", ex);
        }
    }
}
