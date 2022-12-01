using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace LitterService.Application.Contracts.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(string id);
        Task<TEntity> GetWithFilterAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}