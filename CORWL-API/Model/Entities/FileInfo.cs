using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CORWL_API.Model.Entities
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
        [StringLength(8)]
        public string fileExtension { get; set; }

        [Required]
        [Precision(18, 4)]
        public string FileUnit { get; set; }

        [Required]
        [StringLength(521)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(16)]
        public string ContentType { get; set; }

        [NotMapped]
        public byte[] Content { get; set; }


    }
}
