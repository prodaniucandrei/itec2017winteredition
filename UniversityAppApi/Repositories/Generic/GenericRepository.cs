using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;

namespace UniversityAppApi.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected UniversityContext _ctx;

        protected DbSet<T> _dbSet;

        public GenericRepository(UniversityContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _dbSet.FirstOrDefault(a => a.Id == id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
        }

        public virtual T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Param is null");
            }
            _dbSet.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Param is null");
            }
            await _dbSet.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Param is null");
            }
            var existing = Get(entity.Id);
            if (existing != null)
            {
                _ctx.Entry(existing).CurrentValues.SetValues(entity);
                _ctx.SaveChanges();
                return existing;
            }
            return null;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Param is null");
            }
            var existing = await GetAsync(entity.Id);
            if (existing != null)
            {
                _ctx.Entry(existing).CurrentValues.SetValues(entity);
                await _ctx.SaveChangesAsync();
                return existing;
            }
            return null;
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Param is null");
            }
            var existing = Get(entity.Id);
            if (existing != null)
            {
                _ctx.Remove(existing);
                _ctx.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("Id is null");
            }
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Param is null");
            }
            var existing = await GetAsync(entity.Id);
            if (existing != null)
            {
                _ctx.Remove(existing);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new ArgumentNullException("Id is null");
            }
        }

        public async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }
    }
}
