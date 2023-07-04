
using Microsoft.AspNetCore.Identity;

namespace CORWL_API.Model.Entities
{

    public class Role : IdentityRole<int>
    {
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public int? CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string? LastUpdatedComment { get; set; }
        public int? UpdatedBy { get; set; }
        public int UpdatedCount { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
