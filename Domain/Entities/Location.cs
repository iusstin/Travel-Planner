using Domain.Common;

namespace Domain.Entities;

public class Location : BaseEntity
{
    public long Id { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string Country { get; set; } = default!;
}
