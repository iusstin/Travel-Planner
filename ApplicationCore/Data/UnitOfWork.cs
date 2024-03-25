using Domain.Interfaces;

namespace ApplicationCore.Data;

public class UnitOfWork(IAppDbContext context) : IUnitOfWork
{
    private readonly IAppDbContext _context = context;

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
