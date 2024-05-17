namespace Domain.Models;

public class CreatePlaceModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? Link { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Image { get; set; }
    public decimal? Cost { get; set; }
    public string AverageDuration { get; set; }
    public string Currency { get; set; }
    public long? DestinationId { get; set; }
}
