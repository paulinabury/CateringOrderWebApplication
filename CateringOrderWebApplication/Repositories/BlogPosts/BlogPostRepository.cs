using Azure;
using CateringOrderWebApplication.Data;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Repositories.BlogPosts
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
            return await _dbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<BlogPost?> GetAsync(string urlHandle)
        {
            var existingBlogPost = _dbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);

            return existingBlogPost ?? null;
        }

        public async Task<BlogPost> AddAsync(BlogPost newBlogPost)
        {
            await _dbContext.BlogPosts.AddAsync(newBlogPost);
            await _dbContext.SaveChangesAsync();

            return newBlogPost;
        }

        public async Task<BlogPost>? EditAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _dbContext.BlogPosts
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Id = blogPost.Id;
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Contet = blogPost.Contet;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.IsVisible = blogPost.IsVisible;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Tags = blogPost.Tags;

                await _dbContext.SaveChangesAsync();

                return existingBlogPost;
            }

            return null;
        }

        public async Task<BlogPost>? DeleteAsync(Guid id)
        {
            var existingBlogPost = await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
                _dbContext.BlogPosts.Remove(existingBlogPost);
                await _dbContext.SaveChangesAsync();

                return existingBlogPost;
            }

            return null;
        }
    }
}