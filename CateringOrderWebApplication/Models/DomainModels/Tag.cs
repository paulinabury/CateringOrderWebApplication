using System.ComponentModel.DataAnnotations;

namespace CateringOrderWebApplication.Models.DomainModels
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