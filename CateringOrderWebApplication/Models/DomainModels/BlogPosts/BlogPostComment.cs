using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Models.DomainModels.BlogPosts
{
    public class BlogPostComment : IEntityBase
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}