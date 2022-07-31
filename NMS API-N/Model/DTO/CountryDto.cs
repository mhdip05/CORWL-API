using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class CountryDto : CommonDto
    {
        public int CountryId { get; set; }
#nullable disable
        [Required(ErrorMessage = "Country name is required")]
        [MinLength(3, ErrorMessage = "Country Name must be minimum of 3 character")]
        [MaxLength(56, ErrorMessage = "Country Name must be maximum of 56 character")]

        public string CountryName { get; set; }

    }
}
