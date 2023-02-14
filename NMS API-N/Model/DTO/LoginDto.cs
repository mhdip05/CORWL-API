using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class LoginDto
    {
#nullable disable
        [Required]
        [MaxLength(100, ErrorMessage = "Username must be maximum of length 100 character")]
        [MinLength(2, ErrorMessage = "Username must be minimum of length of 2")]
        public string Username { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Password must be minimum length of 5")]
        public string Password { get; set; }
    }
}
