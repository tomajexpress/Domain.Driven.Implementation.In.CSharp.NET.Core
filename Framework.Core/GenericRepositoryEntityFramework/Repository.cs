using Microsoft.EntityFrameworkCore;
using SharedKernel.Models;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GenericRepositoryEntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected readonly DbContext Context;

        private readonly DbSet<TEntity> _dbSet;


        public Repository(DbContext context)
        {
            Context = context;

            if (context != null)
            {
                _dbSet = context.Set<TEntity>();
            }
        }


        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }


        public virtual void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }


        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            IQueryable<TEntity> query = _dbSet.Include(include);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate).ConfigureAwait(false);
        }


        public virtual async Task<QueryResult<TEntity>> GetPageAsync(QueryObjectParams queryObjectParams)
        {
            return await GetOrderedPageQueryResultAsync(queryObjectParams, _dbSet).ConfigureAwait(false);
        }


        public virtual async Task<QueryResult<TEntity>> GetPageAsync(QueryObjectParams queryObjectParams, Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return await GetOrderedPageQueryResultAsync(queryObjectParams, query).ConfigureAwait(false);
        }

        public virtual async Task<QueryResult<TEntity>> GetPageAsync(QueryObjectParams queryObjectParams, List<Expression<Func<TEntity, object>>> includes)
        {
            IQueryable<TEntity> query = _dbSet;

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await GetOrderedPageQueryResultAsync(queryObjectParams, query).ConfigureAwait(false);
        }



        public virtual async Task<QueryResult<TEntity>> GetPageAsync<TProperty>(QueryObjectParams queryObjectParams, Expression<Func<TEntity, bool>> predicate, List<Expression<Func<TEntity, TProperty>>> includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await GetOrderedPageQueryResultAsync(queryObjectParams, query).ConfigureAwait(false);
        }



        public virtual async Task<QueryResult<TEntity>> GetOrderedPageQueryResultAsync(QueryObjectParams queryObjectParams, IQueryable<TEntity> query)
        {
            IQueryable<TEntity> OrderedQuery = query;

            if (queryObjectParams.SortingParams != null && queryObjectParams.SortingParams.Count > 0)
            {
                OrderedQuery = SortingExtension.GetOrdering(query, queryObjectParams.SortingParams);
            }

            var totalCount = await query.CountAsync().ConfigureAwait(false);

            if (OrderedQuery != null)
            {
                var fecthedItems = await GetPagePrivateQuery(OrderedQuery, queryObjectParams).ToListAsync().ConfigureAwait(false);

                return new QueryResult<TEntity>(fecthedItems, totalCount);
            }

            return new QueryResult<TEntity>(await GetPagePrivateQuery(_dbSet, queryObjectParams).ToListAsync().ConfigureAwait(false), totalCount);
        }


        private IQueryable<TEntity> GetPagePrivateQuery(IQueryable<TEntity> query, QueryObjectParams queryObjectParams)
        {
            return query.Skip((queryObjectParams.PageNumber - 1) * queryObjectParams.PageSize).Take(queryObjectParams.PageSize);
        }

    }
}
