using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class CurrencyDto
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Currency is Required")]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "Currency name must be between {2} and {1} characters")]
        [RegularExpression(@"^[a-zA-Z ']+$", ErrorMessage = "Invalid currency, only (a-z, A-Z) are allowed")]
        public string CurrencyName { get; set; }

        [StringLength(128, MinimumLength = 2, ErrorMessage = "Currency details must be between {2} and {1} characters")]
        public string Details { get; set; }

        [StringLength(3, MinimumLength = 1, ErrorMessage = "Currency sumbol must be between {2} and {1} characters")]
        [RegularExpression(@"\p{Sc}", ErrorMessage = "Invalid Currency, only currency symbol are allowed")]
        public string CurrencySymbol { get; set; }
    }
}
