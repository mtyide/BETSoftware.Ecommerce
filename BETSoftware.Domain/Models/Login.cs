using System.ComponentModel.DataAnnotations;

namespace BETSoftware.Domain.Models
{
    public class Login
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(75)]
        public string Username { get; set; }

        [Required]
        [MaxLength(13)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(75)]
        public string EmailAddress { get; set; }
    }
}
