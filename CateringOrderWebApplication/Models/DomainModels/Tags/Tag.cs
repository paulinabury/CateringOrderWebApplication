using System.ComponentModel.DataAnnotations;
using CateringOrderWebApplication.Models.DomainModels.BlogPosts;
using CateringOrderWebApplication.Repositories.Base;

namespace CateringOrderWebApplication.Models.DomainModels.Tags
{
    public class Tag : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}