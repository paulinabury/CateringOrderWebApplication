using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public BlogPostLikeRepository(CateringOrderDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await _dbContext.BlogPostLikes
                .CountAsync(x => x.BlogPostId == blogPostId);
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _dbContext.BlogPostLikes.AddAsync(blogPostLike);
            await _dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await _dbContext
                .BlogPostLikes
                .Where(x => x.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}