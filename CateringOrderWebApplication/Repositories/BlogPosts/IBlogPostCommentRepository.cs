using CateringOrderWebApplication.Models.DomainModels.BlogPosts;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

        Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId);
    }
}