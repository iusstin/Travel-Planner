using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly IAppDbContext _context;

    public PlaceRepository(IAppDbContext context)
        => _context = context;

    public async Task CreatePlace(Place place, CancellationToken cancellationToken)
    {
        place.CreateDate = DateTime.Now;
        place.LastModifiedDate = DateTime.Now;
        await _context.Places.AddAsync(place, cancellationToken);
    }

    public async Task<IEnumerable<Place>> GetAllPlaces(CancellationToken cancellationToken)
    {
        var places = QueryPlaces();
        return await places.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Place>> GetByExpression(Expression<Func<Place, bool>>? filter, Func<IQueryable<Place>, IIncludableQueryable<Place, object>>? include = null, Func<IQueryable<Place>, IOrderedQueryable<Place>>? orderBy = null, bool disableTracking = true, bool ignoreQueryFilters = false, CancellationToken cancellationToken = default)
    {
        var places = QueryPlaces(filter, include, orderBy, disableTracking, ignoreQueryFilters);
        return await places.ToListAsync(cancellationToken);
    }

    public async Task<Place?> GetPlaceById(long id)
    {
        var place = await _context.Places.FirstOrDefaultAsync(p => p.Id == id);
        return place;
    }

    public async Task<Place> UpdatePlace(Place place, CancellationToken cancellationToken)
    {
        place.LastModifiedDate = DateTime.Now;
        _context.Places.Attach(place);
        return await Task.FromResult(place);
    }

    private IQueryable<Place> QueryPlaces(
        Expression<Func<Place, bool>>? filter = null,
        Func<IQueryable<Place>, IIncludableQueryable<Place, object>>? include = null,
        Func<IQueryable<Place>, IOrderedQueryable<Place>>? orderBy = null,
        bool trackChanges = false,
        bool ignoreQueryFilters = false)
    {
        var places = _context.Places.AsQueryable();
        if (!trackChanges)
            places = places.AsNoTracking();

        if (ignoreQueryFilters)
            places = places.IgnoreQueryFilters();

        if (include is not null)
            places = include(places);

        if (filter is not null)
            places = places.Where(filter);

        if (orderBy is not null)
            places = orderBy(places);

        return places;
    }
}
