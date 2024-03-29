﻿using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Models.Response;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Services;

public interface IUserService
{
    public User Create(UserRequest newUserRequest);
    public User GetById(int id);
    public List<UserLeaderboardResponse> GetUserLeaderboard();
}

public class UserService : IUserService
{
    private readonly IUserRepo _users;

    public UserService(IUserRepo users)
    {
        _users = users;
    }

    public User Create(UserRequest newUserRequest)
    {
        return _users.Create(newUserRequest);
    }

    public User GetById(int id)
    {
        return _users.GetById(id);
    }

    public List<UserLeaderboardResponse> GetUserLeaderboard()
    {
        return _users.GetUserLeaderboard();
    }
}
