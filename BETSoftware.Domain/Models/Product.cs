using System.ComponentModel.DataAnnotations;

namespace BETSoftware.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(75)]
        public int Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(500)]
        public string? ImageUri { get; set; }
        public bool? Active { get; set; }
        public decimal? Price { get; set; }
    }
}
