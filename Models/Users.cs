using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Required, MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength(15), MinLength(5)]
        public string? Password { get; set; }
  
        public bool Captcha { get; set; }
    }
}
