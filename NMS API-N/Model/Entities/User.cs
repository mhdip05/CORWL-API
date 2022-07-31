using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class User : IdentityUser<int>
    {
        public int EmployeeId { get; set; }
        public DateTime? LastActive { get; set; } = DateTime.Now;
        public DateTime? PasswordUpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? LastUpdatedComment { get; set; }
        public int UpdatedCount { get; set; } = 0;
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeletedDate { get; set; }

#nullable disable

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public int CompanyId { get; set; }


    }
}
