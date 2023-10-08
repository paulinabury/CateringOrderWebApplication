using Azure;
using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly CateringOrderDbContext _dbContext;

        public BlogPostRepository(CateringOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _dbContext.BlogPosts
                .Include(x => x.Tags)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await _dbContext.BlogPosts.FindAsync(id);
        }

        public async Task<BlogPost> AddAsync(BlogPost newBlogPost)
        {
            await _dbContext.BlogPosts.AddAsync(newBlogPost);
            await _dbContext.SaveChangesAsync();

            return newBlogPost;
        }

        public Task<BlogPost>? EditAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost>? DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}