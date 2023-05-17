using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class ResetPasswordDto
    {
#nullable disable
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the code that sent to your email.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(5, ErrorMessage = "New password must be at least 5 character.")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passowrd does not match with confirm passowrd.")]
        public string ConfirmPassword { get; set; }
    }
}
