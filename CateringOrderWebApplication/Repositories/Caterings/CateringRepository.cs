using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.Caterings;
using CateringOrderWebApplication.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.Caterings
{
    public class CateringRepository : EntityBaseRepository<Catering>, ICateringRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public CateringRepository(CateringOrderDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Catering>> GetAllAsync()
        {
            return await _dbContext.Caterings
                .Include(x => x.Tags)
                .ToListAsync();
        }

        public async Task<Catering?> GetAsync(Guid id)
        {
            return await _dbContext.Caterings
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Catering?> EditAsync(Catering catering)
        {
            var existingCatering = await _dbContext.Caterings
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == catering.Id);

            if (existingCatering == null) return null;

            existingCatering.Id = catering.Id;
            existingCatering.Name = catering.Name;
            existingCatering.ShortDescription = catering.ShortDescription;
            existingCatering.Content = catering.Content;
            existingCatering.Price = catering.Price;
            existingCatering.Tags = catering.Tags;

            await _dbContext.SaveChangesAsync();
            return existingCatering;
        }
    }
}