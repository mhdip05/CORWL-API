using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class RoleDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        [MinLength(2, ErrorMessage = "Minimum length 2")]
        [MaxLength(50, ErrorMessage = "Maximum length 50")]

#nullable disable
        public string RoleName{ get; set; }
    }
}
