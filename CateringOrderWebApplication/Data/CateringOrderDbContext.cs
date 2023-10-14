using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using Microsoft.EntityFrameworkCore;

namespace CateringOrderWebApplication.Data
{
    public class CateringOrderDbContext : DbContext
    {
        public CateringOrderDbContext(DbContextOptions<CateringOrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
    }
}