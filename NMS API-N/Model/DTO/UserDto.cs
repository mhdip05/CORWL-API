using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    // Return Value for User
    public class UserDto
    {
        public int UserId { get; set; }
        //public int Id { get; set; }

#nullable disable
        [Required(ErrorMessage = "Username Is Required")]
        [MinLength(3, ErrorMessage = "Username can not be less than 3 character")]
        [MaxLength(128, ErrorMessage = "Username can not be more than 128 character")]
        public string Username { get; set; }
        public string Token { get; set; }
        public int EmployeeId { get; set; }
    }
}
