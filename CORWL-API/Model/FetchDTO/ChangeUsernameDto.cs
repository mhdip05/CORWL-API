using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.FetchDTO
{
    public class ChangeUsernameDto
    {
#nullable disable
        [Required(ErrorMessage = "Please enter username")]
        [RegularExpression(@"^[a-zA-Z0-9 ']+$", ErrorMessage = "Only A-Z and a-z combination are allowed")]
        [MaxLength(100, ErrorMessage = "Username must be maximum of length 100 characters")]
        [MinLength(4, ErrorMessage = "Username must be minimum of 4 characters")]
        public string Username { get; set; }
    }
}
