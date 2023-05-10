using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MimeKit;

namespace NMS_API_N.Controllers
{
    public class FileController : BaseApiController
    {
#nullable disable
        private readonly IWebHostEnvironment _env;
        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("GetFile/{directory}/{fileName}/{subdirectory?}")]
        public IActionResult GetFile(string directory, string fileName, string subdirectory=null)
        {
            string filePath;

            if (string.IsNullOrWhiteSpace(subdirectory))
            {
                filePath = Path.Combine(_env.WebRootPath, "FileUpload", directory.Trim(), fileName);
            }
            else
            {
                filePath = Path.Combine(_env.WebRootPath, "FileUpload", directory.Trim(), subdirectory.Trim(), fileName.Trim());
            }

            var fileProvider = new PhysicalFileProvider(Path.GetDirectoryName(filePath));
            var fileInfo = fileProvider.GetFileInfo(Path.GetFileName(filePath));

            if (!fileInfo.Exists)
            {
                return NotFound();
            }

            var fileStream = fileInfo.CreateReadStream();
            var contentType = MimeTypes.GetMimeType(filePath);

            return File(fileStream, contentType);
        }

    }
}
