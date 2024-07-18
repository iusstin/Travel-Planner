namespace Domain.Entities;

public class TripPlace
{
    public long TripId { get; set; }
    public Trip Trip { get; set; }

    public long PlaceId { get; set; }
    public Place Place { get; set; }
}
