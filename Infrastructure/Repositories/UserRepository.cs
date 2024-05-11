using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IAppDbContext _context;

    public UserRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = QueryUsers();
        return await users.ToListAsync(cancellationToken);
    }

    public Task<User?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetByExpressionAsync(
        Expression<Func<User, bool>>? filter,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false, 
        CancellationToken cancellationToken = default)
    {
        var users = QueryUsers(filter, include, orderBy, disableTracking, ignoreQueryFilters);
        return await users.ToListAsync(cancellationToken);
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _context.Users.AnyAsync(u => u.Email == email);
    }

    private IQueryable<User> QueryUsers(
        Expression<Func<User, bool>>? filter = null,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        bool trackChanges = false,
        bool ignoreQueryFilters = false)
    {
        var users = _context.Users.AsQueryable();
        if (!trackChanges)
            users = users.AsNoTracking();

        if (ignoreQueryFilters)
            users = users.IgnoreQueryFilters();

        if (include is not null)
            users = include(users);

        if (filter is not null)
            users = users.Where(filter);

        if (orderBy is not null)
            users = orderBy(users);

        return users;
    }
}
