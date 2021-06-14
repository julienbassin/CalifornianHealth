using CalendarApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CalendarApi.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntityBase
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(object id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(object id);
        Task UpdateAsync(TEntity entity);
    }
}