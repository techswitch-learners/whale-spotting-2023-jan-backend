using System.Linq;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WhaleSpotting.Repositories;

public interface IUserRepo
{
    public User Create(UserRequest newUserRequest);
    public User GetById(int id);
    public User GetByUsername(string username);
    public List<UserLeaderboardResponse> GetUserLeaderboard();
}

public class UserRepo : IUserRepo
{
    private readonly WhaleSpottingDbContext _context;

    public UserRepo(WhaleSpottingDbContext context)
    {
        _context = context;
    }

    public User Create(UserRequest newUserRequest)
    {
        if (_context.Users.Any(u => u.Username == newUserRequest.Username))
        {
            throw new ArgumentOutOfRangeException(nameof(newUserRequest),
                $"The given username {newUserRequest.Username} is already present in the database");
        }
        var newUser = new User
        {
            Username = newUserRequest.Username.ToLower() ??
                       throw new ArgumentNullException(nameof(newUserRequest),
                           "Property \"Username\" must not be null"),
            Password = newUserRequest.Password ??
                       throw new ArgumentNullException(nameof(newUserRequest),
                           "Property \"Password\" must not be null"),
            ProfileImageUrl = newUserRequest.ProfileImageUrl,
            UserBio = newUserRequest.UserBio,
            UserType = newUserRequest.UserType,
        };
        var insertedEntity = _context.Users.Add(newUser);
        _context.SaveChanges();

        return insertedEntity.Entity;
    }

    public User GetById(int id)
    {
        try
        {
            return _context.Users.Single(u => u.Id == id);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No user with id {id} found in the database", ex);
        }
    }

    public User GetByUsername(string username)
    {
        try
        {
            return _context.Users.Single(u => u.Username == username);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No user with username {username} found in the database", ex);
        }
    }

    public List<UserLeaderboardResponse> GetUserLeaderboard()
    {
                return _context.Users
                    .Include(u => u.WhaleSighting)
                    .ThenInclude(ws=> ws.Likes)
                    .Select(x => new UserLeaderboardResponse(x))
                    .AsEnumerable()
                    .OrderByDescending(r=>r.NumberOfWhaleSightings)
                    .ToList(); 
    }
}
