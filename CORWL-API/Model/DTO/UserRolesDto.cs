using CORWL_API.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class UserRolesDto
    {
#nullable disable
        public int EmployeeId { get; set; }

        public List<int> RoleIds { get; set; }
    }

}
