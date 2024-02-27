using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class LoginDto
    {
#nullable disable
        [Required]
        //[MaxLength(100, ErrorMessage = "Username must be maximum of length 100 characters")]
        //[MinLength(2, ErrorMessage = "Username must be minimum of 2 characters")]
        public string Username { get; set; }

        [Required]
        //[MinLength(5, ErrorMessage = "Password must be minimum of 5 charcters")]
        public string Password { get; set; }
    }
}
