using System.ComponentModel.DataAnnotations;

namespace Catllery.Models
{
    public class CatPhoto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Photo URL")]
        public string Url { get; set; } = string.Empty; // Add a default value

        [Required]
        [Display(Name = "Photo Caption")]
        public string Caption { get; set; } = "I am the best cat in the world";
    }
}