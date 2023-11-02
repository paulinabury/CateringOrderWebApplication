using System.ComponentModel.DataAnnotations;
using CateringOrderWebApplication.Models.DomainModels.Tags;

namespace CateringOrderWebApplication.Models.DomainModels.Caterings
{
    public class Catering
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string FeaturedImageUrl { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}