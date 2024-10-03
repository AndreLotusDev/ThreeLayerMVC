using System.Linq.Expressions;
using ThreeLayer.Business.Models;

namespace ThreeLayer.Business.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        List<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
