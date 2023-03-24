using System.ComponentModel.DataAnnotations.Schema;

namespace WhaleSpotting.Models.Database;

public class Like
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int WhaleSightingId { get; set; }
    [ForeignKey("WhaleSightingId")]
    public WhaleSighting WhaleSighting { get; set; } = null!;
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
}
