using System.ComponentModel.DataAnnotations;

namespace CRUD_Class.Models
{
    public class Sliders
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string RedirectUrl { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string RedirectText { get; set; }
    }
}
