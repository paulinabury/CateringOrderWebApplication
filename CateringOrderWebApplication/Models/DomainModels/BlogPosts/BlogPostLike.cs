using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Models.DomainModels.BlogPosts
{
    public class BlogPostLike : IEntityBase
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}