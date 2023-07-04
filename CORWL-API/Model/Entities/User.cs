using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORWL_API.Model.Entities { 

    public class User : IdentityUser<int>
    {
#nullable disable
        public int EmployeeId { get; set; }
        public DateTime? LastActive { get; set; } = DateTime.Now;
        public DateTime? PasswordUpdatedDate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastUpdatedComment { get; set; }
        public int UpdatedCount { get; set; } = 0;
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int EmailCode { get; set; }

        [StringLength(256)]
        public string UserPrimaryPhotoPath { get; set; }

    }
}
