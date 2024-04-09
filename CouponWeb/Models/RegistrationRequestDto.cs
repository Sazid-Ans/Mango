using System.ComponentModel.DataAnnotations;

namespace MangoWeb.Models
{
    public class RegistrationRequestDto
    {
        [Required]
        [Display(Name = "Name")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string? UserPhoneNumber { get; set; }

        [Required]
        public string? password { get; set; }
        public string? UserRole  { get; set; }
    }
}
