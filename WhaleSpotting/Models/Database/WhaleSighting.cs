using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpotting.Models.Database;

public class WhaleSighting
{
    public int Id { get; set; }
    public DateTime DateOfSighting { get; set; }
    public float LocationLatitude { get; set; }
    public float LocationLongitude { get; set; }
    public string PhotoImageURL { get; set; }
    public int NumberOfWhales { get; set; }
    public ApprovalStatus ApprovalStatus {get;set;}
    public string Description { get; set; }
    public WhaleSpecies WhaleSpecies { get; set; }
    // [ForeignKey("User")]
    // public int UserId { get;set;}
    public User User { get; set; }
    public ICollection<Like> Likes { get; set; }
}
