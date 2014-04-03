using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public interface IGenericRepository
    {
        Task<List<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class;

        Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class;

        Task<Int32> InsertAsync<TEntity>(TEntity entity) where TEntity : class;

        Task<Int32> Delete<TEntity>(object id) where TEntity : class;

        Task<Int32> Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
    }

    public class GenericRepository : IGenericRepository
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> FindAllAsync<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : class
        {
            return await _context.Set<TEntity>().Where(match).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<Int32> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<Int32> Delete<TEntity>(object id) where TEntity : class
        {
            var entityToDelete = _context.Set<TEntity>().Find(id);
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entityToDelete);
            }
            _context.Entry(entityToDelete).State = EntityState.Deleted;
            return await _context.SaveChangesAsync();
        }


        public async Task<Int32> Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

    }
}
