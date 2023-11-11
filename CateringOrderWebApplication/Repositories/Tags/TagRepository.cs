using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Repositories.Tags
{
    public class TagRepository : EntityBaseRepository<Tag>, ITagRepository
    {
        public TagRepository(CateringOrderDbContext dbContext) : base(dbContext)
        { }
    }
}