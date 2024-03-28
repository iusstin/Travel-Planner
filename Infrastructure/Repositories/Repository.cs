using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : IBaseEntity
{
    protected readonly DbContext _dbContext;

    protected Repository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<TEntity> Add(TEntity entity)
    {
        entity.CreateDate = DateTime.Now;
        //var I pt adaugare, ef isi da seama despre ce dbset e vorba pt salvarea entitatii
        //await _dbContext.AddAsync(entity);
        // sau varianta II
        await _dbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        var entities = _dbContext.Set<TEntity>().AsQueryable();
        return await entities.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> SelectAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false,
        CancellationToken cancellationToken = default)
    {
        var entities = QueryEntities(predicate, include, orderBy, disableTracking, ignoreQueryFilters);
        return await entities.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var entity = QueryEntities(filter: e => e.Id == id, include: include);
        return await entity.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<TEntity?> GetByExpressionAsync(
        Expression<Func<TEntity, bool>>? filter,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool disableTracking = true,
        bool ignoreQueryFilters = false)
    {
        var entity = QueryEntities(filter, include, null, disableTracking, ignoreQueryFilters);
        return await entity.FirstOrDefaultAsync();
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        var existingEntity = await GetByIdAsync(entity.Id);
        existingEntity.LastModifiedDate = DateTime.Now;
        _dbContext.Set<TEntity>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        return await Task.FromResult(entity);
    }

    public virtual async Task Delete(int id, bool softDelete = false)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
            return;

        if (softDelete)
        {
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            await Update(entity);
        }
        else
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }

    public bool HasPendingChanges()
    {
        var changes = _dbContext.ChangeTracker
            .Entries()
            .Count(e => e.State is EntityState.Added
                or EntityState.Deleted
                or EntityState.Modified);
        return changes > 0;
    }

    public async Task ApplyChanges()
    {
        if (HasPendingChanges())
            await _dbContext.SaveChangesAsync();
    }

    private IQueryable<TEntity> QueryEntities(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool trackChanges = false,
        bool ignoreQueryFilters = false)
    {
        var entities = _dbContext.Set<TEntity>().AsQueryable();
        if (!trackChanges)
            entities = entities.AsNoTracking();

        if (ignoreQueryFilters)
            entities = entities.IgnoreQueryFilters();

        if (include is not null)
            entities = include(entities);

        if (filter is not null)
            entities = entities.Where(filter);

        if (orderBy is not null)
            entities = orderBy(entities);

        return entities;
    }
}

