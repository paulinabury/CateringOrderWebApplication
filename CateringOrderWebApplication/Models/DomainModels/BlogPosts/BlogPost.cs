using System.ComponentModel.DataAnnotations;
using CateringOrderWebApplication.Models.DomainModels.Tags;

namespace CateringOrderWebApplication.Models.DomainModels.BlogPosts
{
    public class BlogPost
    {
        [Key]
        public Guid Id { get; set; }

        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Contet { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        // Navigation properties
        public ICollection<Tag> Tags { get; set; }

        public ICollection<BlogPostLike> Likes { get; set; }
        public ICollection<BlogPostComment> Comments { get; set; }
    }
}