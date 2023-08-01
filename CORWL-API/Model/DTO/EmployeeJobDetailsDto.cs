using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class EmployeeJobDetailsDto : CommonDto
    {
#nullable disable
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select company")]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select department")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please select designation")]
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }

        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; }

        public int? StaffGrade { get; set; }

        [Required(ErrorMessage = "Please set confirmation date")]
        public DateTime? ConfirmationDate { get; set; }

        [Required(ErrorMessage = "Please set confirmation date")]
        public DateTime? JoiningDate { get; set; }
        public string ReportingMethod { get; set; }
    }
}

