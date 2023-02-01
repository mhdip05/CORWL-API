using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.FetchDTO
{
    public class ChangeEmailDto
    {
#nullable disable
        [Required(ErrorMessage ="Please enter your email")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))", ErrorMessage = "Please provide valid email address")]
        public string Email { get; set; }
    }
}
