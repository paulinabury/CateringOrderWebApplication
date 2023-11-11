using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public class BlogPostCommentRepository : EntityBaseRepository<BlogPostComment>, IBlogPostCommentRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public BlogPostCommentRepository(CateringOrderDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsByBlogIdAsync(Guid blogPostId)
        {
            return await _dbContext.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}