using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class Currency : CommonField
    {
#nullable disable

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(56)]
        public string CurrencyName { get; set; }

        [StringLength(128)]
        public string Details { get; set; }

        [StringLength(5)]
        public string CurrencySymbol { get; set; }
    }
}
