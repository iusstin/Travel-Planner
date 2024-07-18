using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Trip : BaseEntity
{
    public Trip()
    {
        Mates = new HashSet<TripMate>();
        TripPlaces = new HashSet<TripPlace>();
    }

    [Key]
    public long Id { get; set; }

    public string TripCreatorId { get; set; }
    [ForeignKey("TripCreatorId")]
    public User TripCreator { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = default!;
    [Required]
    public string TripLocation { get; set; } = default!;
    [Required]
    public DestinationType Type { get; set; } = default!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public ICollection<TripMate> Mates { get; set; } = default!;
    public ICollection<TripPlace> TripPlaces { get; set; } = default!;
}
