using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public class BlogPostRepository : EntityBaseRepository<BlogPost>, IBlogPostRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public BlogPostRepository(CateringOrderDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<BlogPost?> GetAsync(string urlHandle)
        {
            var existingBlogPost = _dbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

            return existingBlogPost ?? null;
        }
    }
}