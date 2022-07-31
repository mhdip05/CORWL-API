using System.ComponentModel.DataAnnotations;
using NMS_API_N.Extension;
namespace NMS_API_N.Model.DTO
{
    public class CompanyDto
    {
#nullable disable
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        [MaxLength(100, ErrorMessage = "Maximum length of company name must be 100")]
        [MinLength(1, ErrorMessage = "Minimum length of company name must be 1")]

        public string CompanyName { get; set; } 



    }
}
