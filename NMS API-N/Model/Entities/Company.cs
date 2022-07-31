using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Company 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

#nullable disable
        [Required]
        [StringLength(2556)]
        public string CompanyName { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }

#nullable enable

        public DateTime? LastUpdatedDate { get; set; }
        public string? LastUpdatedComment { get; set; }
        public int? UpdatedBy { get; set; }       
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

#nullable disable
        public int UpdatedCount { get; set; } = 0;

    }
}
