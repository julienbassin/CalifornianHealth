using CalendarApi.Data.Database;
using CalendarApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApi.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
    {
        protected readonly CalendarDBContext dbContext;

        public Repository(CalendarDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                dbContext.Set<TEntity>().Add(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                dbContext.Entry(entity).State = EntityState.Detached;
                throw;
            }

            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            TEntity entry = await GetByIdAsync(entity.Id);

            if (entry != null)
            {
                dbContext.Entry(entry).State = EntityState.Detached;
                dbContext.Set<TEntity>().Update(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            TEntity entity = await dbContext.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                dbContext.Set<TEntity>().Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
