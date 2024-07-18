using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly IAppDbContext _context;

    public LocationRepository(IAppDbContext context) => _context = context;

    public async Task Add(Location location, CancellationToken cancellationToken)
    {
        location.CreateDate = DateTime.Now;
        location.LastModifiedDate = DateTime.Now;
        await _context.Locations.AddAsync(location, cancellationToken);
    }

    public async Task<Location?> GetById(long id)
    {
        var location = await _context.Locations.FindAsync(id);
        return location;
    }

    public async Task<IEnumerable<Location>> GetByExpressionAsync(
        Expression<Func<Location, bool>>? filter, 
        Func<IQueryable<Location>, IIncludableQueryable<Location, object>>? include = null, 
        Func<IQueryable<Location>, IOrderedQueryable<Location>>? orderBy = null, 
        bool disableTracking = true, 
        bool ignoreQueryFilters = false, 
        CancellationToken cancellationToken = default)
    {
        var locations = QueryLocations(filter, include, orderBy, disableTracking, ignoreQueryFilters);
        return await locations.ToListAsync(cancellationToken);
    }

    private IQueryable<Location> QueryLocations(
        Expression<Func<Location, bool>>? filter,
        Func<IQueryable<Location>, IIncludableQueryable<Location, object>>? include = null,
        Func<IQueryable<Location>, IOrderedQueryable<Location>>? orderBy = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false)
    {
        var locations = _context.Locations.AsQueryable();
        if (disableTracking)
            locations = locations.AsNoTracking();

        if (ignoreQueryFilters)
            locations = locations.IgnoreQueryFilters();

        if (include is not null)
            locations = include(locations);

        if (orderBy is not null)
            locations = orderBy(locations);

        return locations;
    }
}
