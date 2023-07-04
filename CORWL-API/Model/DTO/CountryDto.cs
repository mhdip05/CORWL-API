using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class CountryDto : CommonDto
    {
#nullable disable
        public int  Id { get; set; }    
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Country name is required")]
        [StringLength(56, MinimumLength = 3, ErrorMessage = "Country name must be between {2} and {1} characters")]
        [RegularExpression(@"^[a-zA-Z ']*$", ErrorMessage = "Invalid country, only (a-z and A-Z) are allowed")]
        public string CountryName { get; set; }

        [RegularExpression(@"^[a-zA-Z ']*$", ErrorMessage = "Invalid country alias, only (a-z and A-Z) are allowed")]
        public string CountryAlias { get; set; }

        [StringLength(5, MinimumLength = 2, ErrorMessage = "Telephone Code must be between {2} and {1} characters")]
        [RegularExpression(@"^(?:(?![+]{2})[0-9+])*[0-9]$", ErrorMessage = "Invalid Telephone Code, e.g. +123")]
        public string TelephoneCode { get; set; }

    }

}
