using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class UserDataDto : CommonDto
    {
#nullable disable
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [StringLength(56, MinimumLength = 2, ErrorMessage = "Username's length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9 '](?:(?![._]{2,})[a-zA-Z0-9. '])*[a-zA-Z0-9 ']$", ErrorMessage = "Please enter valid user name")]
        public string Username { get; set; }

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))", ErrorMessage = "Please provide valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone No is required")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "Phone number's length: min={2} & max={1}")]
        [RegularExpression(@"^(?:(?![+]{2})[0-9+])*[0-9]$", ErrorMessage = "Invalid Phone No, e.g. +123456789")]
        public string PhoneNumber { get; set; }
        public string UserPrimaryPhotoPath { get; set; }
    }

    public class UserInfoDto : UserDataDto
    {

        [Required(ErrorMessage = "New password is required")]
        [MinLength(5, ErrorMessage = "New password must be at least 5 character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match with confirm passowrd.")]
        public string ConfirmPassword { get; set; }
    }

    public class UserPasswordDto 
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(5, ErrorMessage = "New password must be at least 5 character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password does not match with confirm passowrd.")]
        public string ConfirmPassword { get; set; }
    }
}
