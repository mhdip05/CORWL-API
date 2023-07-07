using CORWL_API.Helper;
using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class CompanyDto
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name's length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9 '](?:(?![.-]{2,})[a-zA-Z0-9. '&-])*[a-zA-Z0-9 ']$", ErrorMessage = "Please enter valid company name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Code is required")]
        [RegularExpression(@"^[a-zA-Z0-9 '-]+$", ErrorMessage = "Please enter valid company code")]
        public string CompanyCode { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select city")]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [StringLength(300, MinimumLength = 4, ErrorMessage = "Address length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9# '](?:[a-zA-Z0-9#\-+*/\,:_;()=., '])*[a-zA-Z0-9. ']$", ErrorMessage = "Please enter valid address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [StringLength(8, MinimumLength = 3, ErrorMessage = "Zip code's length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9 ']*$", ErrorMessage = "Please enter valid zipcode")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "Mobile no's length: min={2} & max={1}")]
        [RegularExpression(@"^(?:(?![+]{2})[0-9+])*[0-9]$", ErrorMessage = "Invalid Mobile No, e.g. +123456789")]
        public string MobileNo { get; set; }

        [StringLength(13, MinimumLength = 8, ErrorMessage = "Phone no's length: min={2} & max={1}")]
        [RegularExpression(@"^(?:(?![+]{2})[0-9+])*[0-9]$", ErrorMessage = "Invalid Phone No, e.g. +123456789")]
        public string PhoneNo { get; set; }

        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))", ErrorMessage = "Please provide valid email address")]
        public string Email { get; set; }

        [RegularExpression(@"^(https?\:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})(\/[\w]*)*$", ErrorMessage = "Invalid Website, e.g.www.something.com")]
        public string Web { get; set; }
        public int? InterNationalCurrencyId { get; set; }
        public string InterNationalCurrencyName { get; set; }
        public int? LocalCurrencyId { get; set; }
        public string LocalCurrencyName { get; set; }
        public decimal? ConversionRate { get; set; }
        public string LogoPath { get; set; }
        public bool? IsActive { get; set; } = false;
        public bool? IsParentCompany { get; set; } = false;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow.AddHours(DateTimeHelper.GetUtcHour());
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string LastUpdatedComment { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
