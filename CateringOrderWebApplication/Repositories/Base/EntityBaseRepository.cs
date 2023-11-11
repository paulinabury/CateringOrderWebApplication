using System.Linq.Expressions;
using CateringOrderWebApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly CateringOrderDbContext _dbContext;

        public EntityBaseRepository(CateringOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T?> query = _dbContext.Set<T>().Where(x => x.Id == id);
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            var entityEntry = _dbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T?> UpdateAsync(Guid id, T entity)
        {
            var entityEntry = _dbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}