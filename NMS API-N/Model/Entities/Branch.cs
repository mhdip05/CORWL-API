using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Branch : CommonFieldWithCompany
    {
#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string BranchName { get; set; }

        [Required]
        [StringLength(256)]
        public string BranchCode { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public int CityId { get; set; }
        public int? BranchInchargeId { get; set; }
        public int? BranchAttentionPersonId { get; set; }
        public int BranchTypeId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Address { get; set; }

    }
}
