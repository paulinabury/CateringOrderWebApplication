using System.ComponentModel.DataAnnotations;

namespace CateringOrderWebApplication.Models.ViewModels.Tags
{
    public class AddTagRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}