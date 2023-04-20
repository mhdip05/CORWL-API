using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MimeKit;

namespace NMS_API_N.Controllers
{
    public class FileController : BaseApiController
    {
        private readonly IWebHostEnvironment _env;
        public FileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("GetFile/{fileName}")]
        public IActionResult GetFile(string path)
        {
            //string filePath;

            //if (subdirectory == "none")
            //{
            //    filePath = Path.Combine(_env.WebRootPath, "FileUpload", directory, fileName);
            //}
            //else
            //{
            //    filePath = Path.Combine(_env.WebRootPath, "FileUpload", directory, subdirectory, fileName);
            //}

            var fileProvider = new PhysicalFileProvider(Path.GetDirectoryName("K:\\Project\\user-mhsporsho\\NMS-API\\NMS API-N\\wwwroot/FileUpload/EmployeeDoc\\empId_0\\My Passport.pdf"));
            var fileInfo = fileProvider.GetFileInfo(Path.GetFileName("K:\\Project\\user-mhsporsho\\NMS-API\\NMS API-N\\wwwroot/FileUpload/EmployeeDoc\\empId_0\\My Passport.pdf"));

            if (!fileInfo.Exists)
            {
                return NotFound();
            }

            var fileStream = fileInfo.CreateReadStream();
            var contentType = MimeTypes.GetMimeType("K:\\Project\\user-mhsporsho\\NMS-API\\NMS API-N\\wwwroot/FileUpload/EmployeeDoc\\empId_0\\My Passport.pdf");

            return File(fileStream, contentType);
        }

    }
}
