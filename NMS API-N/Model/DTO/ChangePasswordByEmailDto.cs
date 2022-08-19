using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class ChangePasswordByEmailDto
    {
#nullable disable
        [Required(ErrorMessage = "New password is required")]
        [MinLength(3, ErrorMessage = "New password must be at least 5 character")]
        public string NewPassword { get; set; } 

        [Compare("NewPassword", ErrorMessage = "Passowrd does not match with confirm passowrd")]
        public string ConfirmPassword { get; set; }
    }
}
