using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CORWL_API.Model.Entities
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

        [StringLength(30)]
        public string BranchType { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(126)]
        public string Web { get; set; }

        [StringLength(512)]
        public string Address { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

    }
}
