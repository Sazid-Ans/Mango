using System.ComponentModel.DataAnnotations;

namespace AuthApi.Model.Dto
{
    public class UserDto
    {
        
        public string UserID { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Display(Name = "Phone Number")]
        public string UserPhoneNumber { get; set; }
    }
}
