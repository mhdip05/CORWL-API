using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class FileInfo
    {
#nullable disable
        [Required]
        [StringLength(256)]
        public string FileName { get; set; }

        [Required]
        [StringLength(6)]
        public string FileType { get; set; }

        [Required]
        [Precision(18, 4)]
        public decimal FileSize { get; set; }

        [Required]
        [Precision(18, 4)]
        public string FileUnit { get; set; }

        [Required]
        [StringLength(521)]
        public string FilePath { get; set; }

        [NotMapped]
        public string ContentType { get; set; }

        [NotMapped]
        public byte[] Content { get; set; }

        [NotMapped]
        public string fileExtension { get; set; }
    }
}
