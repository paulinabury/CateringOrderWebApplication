using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Models.DomainModels.Caterings
{
    public class Rating : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CateringId { get; set; }
        public int Rate { get; set; }
    }
}