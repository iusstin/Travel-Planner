using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(string id);
    Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetByExpressionAsync(
        Expression<Func<User, bool>>? filter,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default);
    Task Add(User user, CancellationToken cancellationToken);
    Task<bool> IsEmailUnique(string email);
}
