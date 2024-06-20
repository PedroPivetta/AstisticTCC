using System.ComponentModel.DataAnnotations;

namespace Final.Models
{
    public class Doctors
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Required, MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength(15), MinLength(5)]
        public string? Password { get; set; }

        [Required]
        public string? Cep { get; set; }

        [Required, MaxLength(6)]
        public string? Crm { get; set; }

        [Required]
        public string? Graduated { get; set; }

        [Required, MaxLength(15)]
        public string? Telephone { get; set; }

        public bool Captcha {  get; set; }
    }
}
