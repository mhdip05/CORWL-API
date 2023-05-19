using NMS_API_N.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class UserRolesDto
    {
#nullable disable
        public int EmployeeId { get; set; }

        public List<int> RoleIds { get; set; }
    }

}
