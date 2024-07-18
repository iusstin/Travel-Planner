using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class TripRepository : ITripRepository
{
    private readonly IAppDbContext _context;

    public TripRepository(IAppDbContext context) 
        => _context = context;

    public async Task<IEnumerable<Trip>> GetByExpression(Expression<Func<Trip, bool>>? filter, Func<IQueryable<Trip>, IIncludableQueryable<Trip, object>>? include = null, Func<IQueryable<Trip>, IOrderedQueryable<Trip>>? orderby = null, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        var trips = QueryTrips();
        return await trips.ToListAsync(cancellationToken);
    }

    private IQueryable<Trip> QueryTrips(
        Expression<Func<Trip, bool>>? filter = null,
        Func<IQueryable<Trip>, IIncludableQueryable<Trip, object>>? include = null,
        Func<IQueryable<Trip>, IOrderedQueryable<Trip>>? orderBy = null,
        bool trackChanges = false,
        bool ignoreQueryFilters = false)
    {
        var trips = _context.Trips.AsQueryable();
        if (!trackChanges)
            trips = trips.AsNoTracking();

        if (ignoreQueryFilters)
            trips = trips.IgnoreQueryFilters();

        if (include is not null)
            trips = include(trips);

        if (filter is not null)
            trips = trips.Where(filter);

        if (orderBy is not null)
            trips = orderBy(trips);

        return trips;
    }
}
