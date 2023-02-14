using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.Entities
{
    public class CommonField
    {
#nullable disable
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatedBy { get; set; }

#nullable enable
        public DateTime? LastUpdatedDate { get; set; }

        [StringLength(256)]
        public string? LastUpdatedComment { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int UpdatedCount { get; set; }

    }
}
