using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public class BlogPostLikeRepository : EntityBaseRepository<BlogPost>, IBlogPostLikeRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public BlogPostLikeRepository(CateringOrderDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
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