namespace CateringOrderWebApplication.Repositories.Base
{
    public interface IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetAsync(Guid id);

        Task<T> AddAsync(T entity);

        Task<T?> EditAsync(T newEntity);

        Task<T?> DeleteAsync(Guid id);
    }
}