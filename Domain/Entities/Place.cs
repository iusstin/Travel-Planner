using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Place : BaseEntity
{
    public Place()
    {
        TripPlaces = new HashSet<TripPlace>();
    }
    public long Id { get; set; }    
    public string Name { get; set; } = default!;
    public long LocationId { get; set; }
    public Location Location { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Image { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Cost { get; set; }
    public string AverageDuration { get; set; } = default!;
    public Currency Currency { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Rating { get; set; }
    public ICollection<TripPlace> TripPlaces { get; set; }
}
