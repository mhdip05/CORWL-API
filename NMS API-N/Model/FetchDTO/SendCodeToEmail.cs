using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.FetchDTO
{
    public class SendCodeToEmail
    {
#nullable disable
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        public string Body { get; set; }


    }
}
