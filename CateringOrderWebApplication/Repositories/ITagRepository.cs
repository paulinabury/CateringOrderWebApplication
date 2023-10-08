using CateringOrderWebApplication.Models.DomainModels.Tags;

namespace CateringOrderWebApplication.Repositories
{
    public interface ITagRepository
	{
		Task<IEnumerable<Tag>> GetAllAsync();

		Task<Tag?> GetAsync(Guid id);

		Task<Tag> AddAsync(Tag newTag);

		Task<Tag?> EditAsync(Tag tag);

		Task<Tag?> DeleteAsync(Guid id);
	}
}