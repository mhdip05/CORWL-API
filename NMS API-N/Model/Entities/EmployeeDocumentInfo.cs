using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class EmployeeDocumentInfo : CommonField
    {
#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string DocumentName { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }

    public class EmployeeDocument : FileInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

  
        [ForeignKey("EmployeeDocumentInfoId")]
        public EmployeeDocumentInfo EmployeeDocumentInfo { get; set; }
        public int EmployeeDocumentInfoId { get; set; }

        
    }
}
