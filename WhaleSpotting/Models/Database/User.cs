using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpotting.Models.Database;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? HashedPassword { get; set; }
    List<WhaleSighting>? WhaleSighting { get; set; }
    public string? UserBio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public UserType UserType { get; set; }
    public string Password
    {
        set
        {
            var hasher = new PasswordHasher<User>();
            HashedPassword = hasher.HashPassword(this, value);
        }
    }

    public bool IsPasswordValid(string password)
    {
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(this, HashedPassword, password);
        return result != PasswordVerificationResult.Failed;
    }
}
