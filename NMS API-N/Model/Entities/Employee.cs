using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Employee : CommonFieldWithCompany
    {
#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        public string MaritalStatus { get; set; }

        [StringLength(56)]
        public string Status { get; set; }

        [Required]
        [StringLength(56)]
        public string IdType { get; set; }

        [Required]
        [StringLength(512)]
        public string IdNo { get; set; }

    }
}
