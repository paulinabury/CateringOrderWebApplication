using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public BlogPostCommentRepository(CateringOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await _dbContext.BlogPostComments.AddAsync(blogPostComment);
            await _dbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await _dbContext.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}