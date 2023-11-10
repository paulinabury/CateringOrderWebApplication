using System.ComponentModel.DataAnnotations;
using CateringOrderWebApplication.Models.DomainModels.Tags;
using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Models.DomainModels.Caterings
{
    public class Catering : IEntityBase
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