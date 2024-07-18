using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetByExpression(
        Expression<Func<Trip, bool>>? filter,
        Func<IQueryable<Trip>, IIncludableQueryable<Trip, object>>? include = null,
        Func<IQueryable<Trip>, IOrderedQueryable<Trip>>? orderby = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default);
}
