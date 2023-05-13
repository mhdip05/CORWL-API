using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class RoleDto
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [MinLength(2, ErrorMessage = "Minimum length 2")]
        [MaxLength(50, ErrorMessage = "Maximum length 50")]
        [RegularExpression(@"^[a-zA-Z '](?:(?![.-]{1,})[a-zA-Z '])*[a-zA-Z ']$", ErrorMessage = "Please enter valid role name")]
        public string RoleName { get; set; }
    }
}
