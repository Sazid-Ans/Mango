using System.ComponentModel.DataAnnotations;

namespace AuthApi.Model.Dto
{
    public class RegistrationRequestDto
    {
        [Display(Name = "Name")]
        public string? UserName { get; set; }

        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        [Display(Name = "Phone Number")]
        public string? UserPhoneNumber { get; set; }
        public string? password { get; set; }
        public string? UserRole  { get; set; }
    }
}
