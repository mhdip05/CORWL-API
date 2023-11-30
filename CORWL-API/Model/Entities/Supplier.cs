using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORWL_API.Model.Entities
{
    public class Supplier : CommonField
    {
#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string SupplierName { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public int CityId { get; set; }

        [Required, StringLength(256)]
        public string AddressLineOne { get; set; }

        [StringLength(256)]
        public string AddressLineTwo { get; set; }

        [Required, StringLength(15)]
        public string PhoneOne { get; set;}

        [StringLength(15)]
        public string PhoneTwo { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(35)]
        public string Web { get; set; }

        [StringLength(20)]
        public string SupplierType { get; set; }

        [Required, StringLength(50)]
        public string SupplierCode { get; set; }
    }
}
