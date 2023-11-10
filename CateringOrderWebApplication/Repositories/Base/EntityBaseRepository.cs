using CateringOrderWebApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task<T> GetAsync(Guid id) => await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> EditAsync(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<T?> DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            EntityEntry entityEntry = _dbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}