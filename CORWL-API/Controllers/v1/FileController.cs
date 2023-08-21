using CORWL_API.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MimeKit;

namespace CORWL_API.Controllers.v1
{
    public class FileController : BaseApiController
    {
#nullable disable
        private readonly IWebHostEnvironment _env;
        private readonly IAzureBlob _azureBlob;

        public FileController(IWebHostEnvironment env, IAzureBlob azureBlob)
        {
            _env = env;
            _azureBlob = azureBlob;
        }

        [HttpGet("GetFile/{directory}/{fileName}/{subdirectory?}")]
        public IActionResult GetFile(string directory, string fileName, string subdirectory = null)
        {
            string filePath;

            if (string.IsNullOrWhiteSpace(subdirectory))
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "FileUpload", directory.Trim(), fileName);
            }
            else
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "FileUpload", directory.Trim(), subdirectory.Trim(), fileName.Trim());
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

        [HttpGet("GetAzureStorageContainerToken")]
        public IActionResult GetAzureStorageContainerToken()
        {
            return Ok(new { AzureBlobContainerToken = _azureBlob.CreateServiceSasForContainer() });
        }

        [HttpGet("DeleteFileFromAzure")]
        public async Task<IActionResult> DeleteFileFromAzure([FromQuery]string directory, string blobName)
        {
            return Ok(await _azureBlob.DeleteFileFromAzureStorage(directory,  blobName));
        }
    }
}
