using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CRUD_Class.Models
{
    public class Features
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Description { get; set; }
        [Required]
        public string Logo { get; set; }

    }
}
