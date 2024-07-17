using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser
{
    public User()
    {
        Trips = new HashSet<Trip>();
    }

    public ICollection<Trip> Trips { get; set; }
}
