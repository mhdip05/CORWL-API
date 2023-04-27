using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class EmployeeBasicInfoDto
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee first name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please enter valid first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Employee last name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name length: min={2} & max={1}")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Please enter valid last name")]
        public string LastName { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/mm/yy}")]
        [Required(ErrorMessage = "Dob is required")]
        public DateTime? Dob { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Martial Status is required")]     
        public string MaritalStatus { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "Id Type is required")]
        public string IdType { get; set; }

        [Required(ErrorMessage = "Id No is required")]
        [StringLength(64, MinimumLength = 2, ErrorMessage = "Id No's length: min={2} & max={1}")]
        public string IdNo { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select company")]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
