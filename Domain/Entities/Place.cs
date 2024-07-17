using Domain.Common;

namespace Domain.Entities;

public class Place : BaseEntity
{
    public Place()
    {
        //Trips = new HashSet<Trip>();
    }
    public long Id { get; set; }    
    public string Name { get; set; } = default!;
    public long LocationId { get; set; }
    public Location Location { get; set; }
    public string? Description { get; set; }
    public string? Link { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Image { get; set; }
    public decimal? Cost { get; set; }
    public string AverageDuration { get; set; } = default!;
    public Currency Currency { get; set; }
    public decimal? Rating { get; set; }
    //public ICollection<Trip> Trips { get; set; }
}
