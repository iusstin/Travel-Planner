
namespace Domain.Entities;

public class Place : IBaseEntity
{
    public long Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public string ModifiedBy { get; set; }
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
    public Currency Currency { get; set; }
    public decimal? Rating { get; set; }
}
