using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpotting.Models.Request;

public class SampleWhaleSightingRequest
{
    public DateTime DateOfSighting { get; set; }
    public float LocationLatitude { get; set; }
    public float LocationLongitude { get; set; }
    public string PhotoImageURL { get; set; }
    public int NumberOfWhales { get; set; }
    public ApprovalStatus ApprovalStatus {get;set;}
    public string Description { get; set; }
    public int WhaleSpeciesId { get; set; }
    public int UserId { get; set; }
}
