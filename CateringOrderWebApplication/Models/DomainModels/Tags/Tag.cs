using System.ComponentModel.DataAnnotations;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;

namespace CateringOrderWebApplication.Models.DomainModels.Tags
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}