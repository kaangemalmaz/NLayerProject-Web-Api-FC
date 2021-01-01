using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Entity.Repository
{
    public interface IRepository<TEntity> where TEntity:class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity,bool>> expression);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);


    }
}
