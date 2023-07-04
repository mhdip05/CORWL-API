using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class DepartmentDto : CommonDto
    {
#nullable disable
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is required")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Department's length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z0-9 '](?:(?![.-]{2,})[a-zA-Z0-9. '&-])*[a-zA-Z0-9 ']$", ErrorMessage = "Please enter valid department name")]
        public string DepartmentName { get; set; }

        [RegularExpression(@"^[a-zA-Z '](?:(?![.-]{2,})[a-zA-Z '&-])*[a-zA-Z ']$", ErrorMessage = "Please enter valid Abbreviation")]
        public string Abbreviation { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9 '\-#]+$", ErrorMessage = "Please enter valid code")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Department's code length: min={2} & max={1}")]
        public string DepartmentCode { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9# '](?:[a-zA-Z0-9#\-+*/\,:_;()=. '])*[a-zA-Z0-9. ']$", ErrorMessage = "Please enter valid address")]
        public string DepartmentAddress { get; set; }

        public int DepartmentHeadId { get; set; }
        public string DepartmentHeadName { get; set; }
    }
}
