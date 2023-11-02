using CateringOrderWebApplication.Models.DomainModels.Caterings;

namespace CateringOrderWebApplication.Repositories.Caterings
{
    public interface ICateringRepository
    {
        Task<IEnumerable<Catering>> GetAllAsync();

        Task<Catering?> GetAsync(Guid id);

        Task<Catering> AddAsync(Catering newCatering);

        Task<Catering?> EditAsync(Catering catering);

        Task<Catering?> DeleteAsync(Guid id);
    }
}