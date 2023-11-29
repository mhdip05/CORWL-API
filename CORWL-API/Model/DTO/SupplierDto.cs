using CORWL_API.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class SupplierDto : CommonDto
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Supplier name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9 '](?:(?![.-]{1,})[a-zA-Z0-9 '])*[a-zA-Z0-9 ']$", ErrorMessage = "Please enter valid name")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Supplier Code is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Supplier Codes length: min={2} & max={1}")]
        public string SupplierCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select country")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "City is required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select city")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [StringLength(300, MinimumLength = 4, ErrorMessage = "Address length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9# '](?:[a-zA-Z0-9#\-+*/\,:_;()=., '])*[a-zA-Z0-9. ']$", ErrorMessage = "Please enter valid address")]
        public string AddressLineOne { get; set; }

        [StringLength(300, MinimumLength = 4, ErrorMessage = "Address 2 length: min={2} & max={1}")]
        public string AddressLineTwo { get; set; }

        [StringLength(13, MinimumLength = 8, ErrorMessage = "Phone 1 no's length: min={2} & max={1}")]
        [RegularExpression(@"^(?:(?![+]{2})[0-9+])*[0-9]$", ErrorMessage = "Invalid Phone No, e.g. +123456789")]
        public string PhoneOne { get; set; }

        [StringLength(13, MinimumLength = 8, ErrorMessage = "Phone 1 no's length: min={2} & max={1}")]
        public string PhoneTwo { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Url(ErrorMessage = "Please enter a valid website URL.")]
        public string Web { get; set; }

        [StringLength(20)]
        public string SupplierType { get; set; }
    }
}
