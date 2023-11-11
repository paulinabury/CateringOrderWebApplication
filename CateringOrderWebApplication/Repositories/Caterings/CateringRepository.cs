using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.Caterings;
using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Repositories.Caterings
{
    public class CateringRepository : EntityBaseRepository<Catering>, ICateringRepository
    {
        public CateringRepository(CateringOrderDbContext dbContext) : base(dbContext)
        { }
    }
}