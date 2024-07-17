using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Location> Locations { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
