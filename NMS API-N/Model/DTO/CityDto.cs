using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class CityDto : CommonDto
    {
#nullable disable
        public int CityId { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [MaxLength(256, ErrorMessage = "Maximum length of city name must be 256")]
        [MinLength(1, ErrorMessage = "Minimum length of city name must be 1")]
        public string CityName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Country Is Required")]
        public int CountryId { get; set; }
    }
}
