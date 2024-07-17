namespace Domain.Models;

public class CreatePlaceModel
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public long? LocationId { get; set; }
    public string Address { get; set; } = default!;
    public string? City { get; set; }
    public string? Region { get; set; }
    public string Country { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? PostalCode { get; set; }
    public string? Link { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Image { get; set; }
    public decimal? Cost { get; set; }
    public string AverageDuration { get; set; } = default!;
    public string Currency { get; set; } = default!;
    public long? DestinationId { get; set; }
}
