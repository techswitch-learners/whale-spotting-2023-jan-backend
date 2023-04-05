using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpotting.Models.Request;

public class WhaleSpeciesRequest
{
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public TailType TailType { get; set; }
    public TeethType TeethType { get; set; }
    public WhaleSize Size { get; set; }
    public string Colour { get; set; }
    public string Location { get; set; }
    public string Diet { get; set; }
}
