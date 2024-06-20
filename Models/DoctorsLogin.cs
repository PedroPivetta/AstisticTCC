using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class DoctorsLogin
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string? Email { get; set; }

        [Required, MaxLength(15), MinLength(5)]
        public string? Password { get; set; }
    }
}
