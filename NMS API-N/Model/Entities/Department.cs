using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Department : CommonField
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
#nullable disable
        [Required]
        [StringLength(128)]
        public string DepartmentName { get; set; }

        [StringLength(128)]
        public string Abbreviation { get; set; }

        [StringLength(28)]
        public string DepartmentCode { get; set; }

        [StringLength(128)]
        public string DepartmentAddress { get; set; }

        public int DepartmentHeadId { get; set; }

    }
}
