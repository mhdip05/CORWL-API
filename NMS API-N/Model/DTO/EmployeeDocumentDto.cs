using System.ComponentModel.DataAnnotations;

namespace NMS_API_N.Model.DTO
{
    public class EmployeeDocumentDto
    {
#nullable disable
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter document name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Doc name's length: min={2} & max={1}")]
        public string  DocumentName { get; set; }

        [MaxLength(512, ErrorMessage = "Description length: max={1}")]
        public string Description { get; set; }

        public int EmployeeId { get; set; }

        //[Required]
        public List<IFormFile> Files { get; set; }

        //public string ContentType { get; set; }
        //public byte[] Content { get; set; }

    }
}
