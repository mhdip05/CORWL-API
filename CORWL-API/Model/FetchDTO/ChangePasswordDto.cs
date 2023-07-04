using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.FetchDTO
{
    public class ChangePasswordDto
    {
#nullable disable
        [Required(ErrorMessage = "Current password is required")]
        [MinLength(3, ErrorMessage = "Current password must be at least 5 character")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(3, ErrorMessage = "New password must be at least 5 character")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passowrd does not match with confirm passowrd")]
        public string ConfirmPassword { get; set; }
    }
}
