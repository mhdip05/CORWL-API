using System.ComponentModel.DataAnnotations.Schema;

namespace CORWL_API.Model.Entities
{
    public class CommonFieldWithCompany : CommonField
    {
#nullable disable
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}
