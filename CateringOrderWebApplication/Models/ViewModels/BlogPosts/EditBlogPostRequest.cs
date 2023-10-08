using Microsoft.AspNetCore.Mvc.Rendering;

namespace CateringOrderWebApplication.Models.ViewModels.BlogPosts
{
    public class EditBlogPostRequest
    {
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

        //Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Collect tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}