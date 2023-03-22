using NMS_API_N.Model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class DesignationDto : CommonDto
    {
#nullable disable
        public int Id { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Designation's length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9 '](?:(?![.-]{2,})[a-zA-Z0-9. '&-])*[a-zA-Z0-9 ']$", ErrorMessage = "Please enter valid designation name")]
        public string DesignationName { get; set; }

        [Required(ErrorMessage = "Abbreviation is required")]
        [RegularExpression(@"^[a-zA-Z '](?:(?![.-]{2,})[a-zA-Z '&-])*[a-zA-Z ']$", ErrorMessage = "Please enter valid Abbreviation")]
        public string Abbreviation { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please Select Department")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
