using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Employee : CommonFieldWithCompany
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

#nullable disable

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [StringLength(256)]
        public string LastName { get; set; }
        public DateTime Dob { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(5)]
        public string BloodGroup { get; set; }

        [StringLength(10)]
        public string MartialStatus { get; set; }

        [StringLength(128)]
        public int IdType { get; set; }

        [StringLength(128)]
        public int IdNo { get; set; }

        [StringLength(56)]
        public string Status { get; set; }


    }
}
