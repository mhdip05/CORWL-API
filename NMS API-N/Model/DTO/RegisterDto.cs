using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class RegisterDto : CommonDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string? UserName { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string? Password { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

    }
}
