using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class CityDto
    {
#nullable disable

        [Range(1, int.MaxValue, ErrorMessage = "Please select country")]
        public int CountryId { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "City is required")]

        [StringLength(100, ErrorMessage = "City name must be between {2} and {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z ']*$", ErrorMessage = "Invalid city, special characters detected")]
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public IEnumerable<object> Cities { get; set; }
    }
}
