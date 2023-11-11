namespace CateringOrderWebApplication.Repositories.Base
{
    public interface IEntityBaseRepository<T>
        where T : class, IEntityBase, new()
    {
        Task<T> AddAsync(T entity);

        Task<T?> DeleteAsync(Guid id);
    }
}