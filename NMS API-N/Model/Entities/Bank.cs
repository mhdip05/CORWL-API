using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Bank
    {
#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string BankName { get; set; }

        [Required]
        [StringLength(256)]
        public string BankAccountNo { get; set; }

        [Required]
        public int SourceId { get; set; }

        [StringLength(128)]
        public string SourceType { get; set; }

        [StringLength(256)]
        public string BankBranch { get; set; }

        [StringLength(521)]
        public string BankAddress { get; set; }

        [StringLength(128)]
        public string SwiftCode { get; set; }
    }
}
