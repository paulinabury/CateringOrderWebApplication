using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public TagRepository(CateringOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _dbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }

        public async Task<Tag> AddAsync(Tag newTag)
        {
            await _dbContext.Tags.AddAsync(newTag);
            await _dbContext.SaveChangesAsync();

            return newTag;
        }

        public async Task<Tag?> EditAsync(Tag tag)
        {
            var existingTag = await _dbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _dbContext.SaveChangesAsync();

                return existingTag;
            }

            return null;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await _dbContext.Tags.FindAsync(id);

            if (existingTag == null) return null;

            _dbContext.Tags.Remove(existingTag);
            await _dbContext.SaveChangesAsync();
            return existingTag;
        }
    }
}