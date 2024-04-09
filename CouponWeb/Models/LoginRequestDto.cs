using System.ComponentModel.DataAnnotations;

namespace MangoWeb.Models 
{ 
    public class LoginRequestDto
    {
        [Required]
        public String UserName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
