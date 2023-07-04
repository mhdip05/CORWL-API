using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.FetchDTO
{
    public class ChangeUsernameDto
    {
#nullable disable
        [Required(ErrorMessage = "Please enter username")]
        [RegularExpression(@"^[a-zA-Z0-9 ']+$", ErrorMessage = "Only A-Z and a-z combination are allowed")]
        public string Username { get; set; }
    }
}
