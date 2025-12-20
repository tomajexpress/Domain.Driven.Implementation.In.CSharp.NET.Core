using Microsoft.EntityFrameworkCore;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepositoryEntityFramework;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity>
    where TEntity : class, IAggregateRoot
{
    protected readonly DbContext Context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual void Add(TEntity entity) => _dbSet.Add(entity);
    public virtual void Update(TEntity entity) => _dbSet.Update(entity);
    public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);

    public async Task<TEntity?> GetByIdAsync(object id) => await _dbSet.FindAsync(id).ConfigureAwait(false);
    public virtual async Task<TEntity?> GetAsync(
    Expression<Func<TEntity, bool>> predicate,
    params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includes is { Length: > 0 })
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync().ConfigureAwait(false);

    public async Task<IEnumerable<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, TProperty>> include)
    {
        return await _dbSet.Include(include).ToListAsync().ConfigureAwait(false);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
        await _dbSet.SingleOrDefaultAsync(predicate).ConfigureAwait(false);

    public virtual async Task<QueryResult<TEntity>> GetPageAsync(
        QueryObjectParams queryObjectParams,
        Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        if (includes is { Length: > 0 })
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        var totalCount = await query.CountAsync().ConfigureAwait(false);

        if (queryObjectParams.SortingParams is { Count: > 0 })
        {
            query = SortingExtension.GetOrdering(query, queryObjectParams.SortingParams);
        }

        var items = await query
            .Skip((queryObjectParams.PageNumber - 1) * queryObjectParams.PageSize)
            .Take(queryObjectParams.PageSize)
            .ToListAsync()
            .ConfigureAwait(false);

        return new QueryResult<TEntity>(items, totalCount);
    }
}