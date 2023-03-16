using NMS_API_N.Helper;

namespace NMS_API_N.Model.DTO
{
    public class CommonDto
    {
        public int CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());
        public int? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? DeletedBy { get; set; }
        public string? DeletedByName { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
