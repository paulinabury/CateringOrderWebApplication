using Microsoft.AspNetCore.Mvc.Rendering;

namespace CateringOrderWebApplication.Models.ViewModels.Caterings
{
    public class EditCateringRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }

        public decimal Price { get; set; }
        public string FeaturedImageUrl { get; set; }

        //Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }

        //Collect tag
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}