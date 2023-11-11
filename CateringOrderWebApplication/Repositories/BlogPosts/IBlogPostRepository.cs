using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Repositories.BlogPosts
{
    public interface IBlogPostRepository : IEntityBaseRepository<BlogPost>
    {
        Task<BlogPost?> GetAsync(string urlHandle);
    }
}