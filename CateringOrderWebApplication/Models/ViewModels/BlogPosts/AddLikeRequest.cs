namespace CateringOrderWebApplication.Models.ViewModels.BlogPosts
{
    public class AddLikeRequest
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}