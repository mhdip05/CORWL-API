using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Country : CommonField
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

#nullable disable
        [Required]
        [StringLength(256)]
        public string CountryName { get; set; }

        [StringLength(6)]
        public string CountryAlias { get; set; }

        [StringLength(10)]
        public string TelephoneCode { get; set; }
    }
}
