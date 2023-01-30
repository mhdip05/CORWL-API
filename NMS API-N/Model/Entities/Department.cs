using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Department : CommonFieldWithCompany
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

#nullable disable
        [Required]
        [StringLength(128)]
        public string DepartmentName { get; set; }

        [StringLength(128)]
        public string Abbreviation { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public int CityId { get; set; }


    }
}
