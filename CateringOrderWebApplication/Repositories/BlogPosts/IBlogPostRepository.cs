using CateringOrderWebApplication.Models.DomainModels.BlogPosts;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost?> GetAsync(string urlHandle);

        Task<BlogPost> AddAsync(BlogPost newBlogPost);

        Task<BlogPost?> EditAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsync(Guid id);
    }
}