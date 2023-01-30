using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Company
    {

#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(32)]
        public string CompanyCode { get; set; }


        [ForeignKey("CityId")]
        public int CityId { get; set; }
        public City City { get; set; }

        [StringLength(512)]
        public string Address { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(256)]
        public string MobileNo { get; set; }

        [StringLength(256)]
        public string PhoneNo { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [DefaultValue("www.test.com")]
        [StringLength(126)]
        public string Web { get; set; }

        public int? InterNationalCurrencyId { get; set; }

        [StringLength(16)]
        public int? LocalCurrencyId { get; set; }

        [Precision(18, 4)]
        public decimal? ConversionRate { get; set; }

        [StringLength(512)]
        public string LogoPath { get; set; }
        public bool IsActive { get; set; } 
        public bool IsParentCompany { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(6);
        public int CreatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

#nullable enable
        public string? LastUpdatedComment { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int UpdatedCount { get; set; }

    }
}
