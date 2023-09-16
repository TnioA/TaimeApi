using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Taime.Application.Data.MySql;

namespace Taime.Application.Utils.Data.MySql
{
    public abstract class RepositoryBase { }

    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
        public Task<TEntity> ReadFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task RemoveAsync(TEntity entity);
    }

    public class MySqlRepositoryBase<TEntity>: RepositoryBase, IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        private MySqlContext _context;
        private DbSet<TEntity> _dbSet;

        public MySqlRepositoryBase(MySqlContext context)
        {
            _context = context ?? throw new ArgumentNullException("nullMySqlContext");
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null) query = query.Where(filter);
            
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public virtual async Task<TEntity> ReadFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await _dbSet.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var data = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var data = _context.Update(entity);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}