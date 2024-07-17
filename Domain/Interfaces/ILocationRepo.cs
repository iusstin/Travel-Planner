using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface ILocationRepo
{
    Task<Location?> GetById(long id);
    Task<IEnumerable<Location>> GetByExpressionAsync(
        Expression<Func<Location, bool>>? filter,
        Func<IQueryable<Location>, IIncludableQueryable<Location, object>>? include = null,
        Func<IQueryable<Location>, IOrderedQueryable<Location>>? orderBy = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default);
    Task Add(Location location, CancellationToken cancellationToken);
}
