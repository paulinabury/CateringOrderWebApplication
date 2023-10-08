using CateringOrderWebApplication.Models.DomainModels;

namespace CateringOrderWebApplication.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Contet { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}