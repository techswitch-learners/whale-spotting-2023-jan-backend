using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;

namespace WhaleSpotting.Repositories;

public interface IUserRepo
{
    public User Create(UserRequest newUserRequest);
    public User GetById(int id);
    public User GetByUsername(string username);
}

public class UserRepo : IUserRepo
{
    private readonly WhaleSpottingDbContext context;

    public UserRepo(WhaleSpottingDbContext context)
    {
        this.context = context;
    }

    public User Create(UserRequest newUserRequest)
    {
        if (context.Users.Any(u => u.Username == newUserRequest.Username))
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
        var insertedEntity = context.Users.Add(newUser);
        context.SaveChanges();

        return insertedEntity.Entity;
    }

    public User GetById(int id)
    {
        try
        {
            return context.Users.Single(u => u.Id == id);
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
            return context.Users.Single(u => u.Username == username);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentOutOfRangeException($"No user with username {username} found in the database", ex);
        }
    }
}
