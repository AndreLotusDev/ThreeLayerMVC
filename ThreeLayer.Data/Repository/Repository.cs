using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ThreeLayer.Business.Interfaces.Repository;
using ThreeLayer.Business.Models;
using ThreeLayer.Data.Context;

namespace ThreeLayer.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            var result = _dbSet.Add(entity);
            _context.SaveChanges();

            Console.WriteLine($"Add result: {result}");
        }

        public async Task AddAsync(TEntity entity)
        {
            var result = _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Add result: {result}");
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public List<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            var result = _dbSet.AsNoTracking().Where(predicate).ToList();
            return result;
        }

        public async Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
            return result;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            var result = _context.SaveChanges();
            Console.WriteLine($"Update result: {result}");
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            var result = await _context.SaveChangesAsync();
            Console.WriteLine($"Update result: {result}");
        }
    }
}
