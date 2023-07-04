using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public record BankDto
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Bank Name is required")]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "Bank name must be between 2 to 56 Characters")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Bank account no is required")]
        [StringLength(128, MinimumLength = 5, ErrorMessage = "Bank account must be between 5 to 128 Characters")]
        public string BankAccountNo { get; set; }
        public int SourceId { get; set; }
        public string SourceType { get; set; }
        [StringLength(128, MinimumLength = 2, ErrorMessage = "Bank name must be between 2 to 56 Characters")]
        public string BankBranch { get; set; }
        public string BankAddress { get; set; }
        public string SwiftCode { get; set; }
    }
}
