using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using CateringOrderWebApplication.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.Tags
{
    public class TagRepository : EntityBaseRepository<Tag>, ITagRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public TagRepository(CateringOrderDbContext dbContext) : base(dbContext)
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
    }
}