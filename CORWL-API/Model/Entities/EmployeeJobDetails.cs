using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.Entities
{
    public class EmployeeJobDetails : CommonFieldWithCompany
    {
#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public int DesignationId { get; set; }
        public int? SupervisorId { get; set; }
        public int? StaffGrade { get; set; }

        [Required]
        public DateTime ConfirmationDate { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [StringLength(16)]
        public string ReportingMethod { get; set; }
    }
}
