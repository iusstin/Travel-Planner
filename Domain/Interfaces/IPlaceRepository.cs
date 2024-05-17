using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IPlaceRepository
{
    Task CreatePlace(Place place, CancellationToken cancellationToken);
    Task<IEnumerable<Place>> GetAllPlaces(CancellationToken cancellationToken);
    Task<Place?> GetPlaceById(long id);
    Task<IEnumerable<Place>> GetByExpression(
        Expression<Func<Place, bool>>? filter,
        Func<IQueryable<Place>, IIncludableQueryable<Place, object>>? include = null,
        Func<IQueryable<Place>, IOrderedQueryable<Place>>? orderBy = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default);
    Task<Place> UpdatePlace(Place place, CancellationToken cancellationToken);
}
