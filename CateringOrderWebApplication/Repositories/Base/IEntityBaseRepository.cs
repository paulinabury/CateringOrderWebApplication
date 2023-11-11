using System.Linq.Expressions;

namespace CateringOrderWebApplication.Repositories.Base
{
    public interface IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<T?> GetByIdAsync(Guid id);

        Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includeProperties);

        Task<T> AddAsync(T entity);

        Task<T?> DeleteAsync(Guid id);

        Task<T?> UpdateAsync(Guid id, T entity);
    }
}