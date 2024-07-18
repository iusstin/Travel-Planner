using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public User()
    {
        OwnTrips = new HashSet<Trip>();
        TripMates = new HashSet<TripMate>();    
    }

    public ICollection<Trip> OwnTrips { get; set; }
    public ICollection<TripMate> TripMates { get; set; }
}
