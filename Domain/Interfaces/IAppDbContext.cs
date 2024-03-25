using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
