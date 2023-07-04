using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORWL_API.Model.Entities
{
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SourceId { get; set; }

#nullable disable
        [Required]
        [StringLength(56)]
        public string SourceType { get; set; }

        [StringLength(56)]
        public string AddressType { get; set; }

        [Required]
        [StringLength(512)]
        public string AddressDescription { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public int CityId { get; set; }

        [Required]
        public string Phone { get; set; }

#nullable enable

        [StringLength(256)]
        public string? ZipCode { get; set; }

        [StringLength(512)]
        public string? Email { get; set; }

        [StringLength(512)]
        public string? Web { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? DeletedBy { get; set; }
        public int UpdatedCount { get; set; } = 0;
    }
}
