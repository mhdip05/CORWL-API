using System.ComponentModel.DataAnnotations;

namespace CORWL_API.Model.DTO
{
    public class EmployeeDocumentDto 
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter document name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Doc name's length: min={2} & max={1}")]
        public string  DocumentName { get; set; }

        [StringLength(256, MinimumLength = 2, ErrorMessage = "Description length: min={2} & max={1}")]
        public string Description { get; set; }

        public int EmployeeId { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        [Required(ErrorMessage = "Please select at least 1 file")]
        public List<IFormFile> Files { get; set; }

    }

    public class EmployeeDocumentMaseterDto : CommonDto
    {
        [Required(ErrorMessage = "Please enter document name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Doc name's length: min={2} & max={1}")]
        public string DocumentName { get; set; }

        [StringLength(256, MinimumLength = 2, ErrorMessage = "Description length: min={2} & max={1}")]
        public string Description { get; set; }

        public int EmployeeId { get; set; }
    }
}
