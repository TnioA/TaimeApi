using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaimeApi.Data.MySql;

namespace TaimeApi.Utils.Data.MySql
{
    public class RepositoryBase { }

    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
        public Task<TEntity> ReadFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task RemoveAsync(TEntity entity);
    }

    public class MySqlRepositoryBase<TEntity>: RepositoryBase, IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        private MySqlProvider _provider;
        private DbSet<TEntity> _context;

        public MySqlRepositoryBase(MySqlProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException("nullMySqlProvider");
            _context = provider.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _context;
            if (filter != null) query = query.Where(filter);
            
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
        }

        public virtual async Task<TEntity> ReadFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _context;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await _context.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var data = await _context.AddAsync(entity);
            await _provider.SaveChangesAsync();
            return data.Entity;
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
            await _provider.SaveChangesAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var data = _context.Update(entity);
            await _provider.SaveChangesAsync();
            return data.Entity;
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
            await _provider.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _provider.SaveChangesAsync();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.RemoveRange(entities);
            await _provider.SaveChangesAsync();
        }

        public void Dispose()
        {
            _provider.Dispose();
        }
    }

    //public class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : class
    //{
    //    private MySqlProvider _context;

    //    public RepositoryBase(MySqlProvider provider)
    //    {
    //        if (provider == null)
    //            throw new ArgumentNullException("nullMySqlProvider");

    //        _context = provider;
    //    }

    //    public async Task<T> Find(int id)
    //    {
    //        return await _context.Set<T>().FindAsync(id);
    //    }

    //    public async Task<List<T>> List()
    //    {
    //        return await _context.Set<T>().ToListAsync();
    //    }

    //    public async Task Add(T item)
    //    {
    //        await _context.Set<T>().AddAsync(item);
    //    }

    //    public void Remove(T item)
    //    {
    //        _context.Set<T>().Remove(item);
    //    }

    //    public void Edit(T item)
    //    {
    //        _context.Entry(item).State = EntityState.Modified;
    //    }

    //    public void Dispose()
    //    {
    //        _context.Dispose();
    //    }
    //}
}