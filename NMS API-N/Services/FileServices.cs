using NMS_API_N.IServices;
using NMS_API_N.Model.Entities;
using System.IO;

namespace NMS_API_N.Services
{
    public class FileServices : IFileServices
    {
#nullable disable
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FileServices(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        private string GetFileExtension(string fileName)
        {
            var lastIndex = fileName.LastIndexOf('.');
            return fileName.Substring(lastIndex + 1);
        }

        private decimal ConvertFileSizeToMb(long fileSize)
        {
            var sizeToMb = fileSize / 1000000;
            string formatNumber = sizeToMb.ToString("0.000");
            return decimal.Parse(formatNumber);
        }

        public async Task<List<Model.Entities.FileInfo>> CopyFileToServer(List<IFormFile> files, string directory, string subdirectory = null, string folderName = null)
        {
            var currentDirectory = Environment.CurrentDirectory;
            var baseFolder = "FileUpload";

            var baseDirectoryPath = subdirectory == null
                ? Path.Combine(currentDirectory, _webHostEnvironment.WebRootPath, baseFolder, directory.ToLower())
                : Path.Combine(currentDirectory, _webHostEnvironment.WebRootPath, baseFolder, directory.ToLower(), subdirectory.ToLower());
            
            var uploadFolderPath = folderName == null 
                 ? baseDirectoryPath
                 : Path.Combine(baseDirectoryPath, folderName);

            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);
            
            List<Model.Entities.FileInfo> fileInfo = new List<Model.Entities.FileInfo>();

            foreach (var file in files)
            {
                string filePath = Path.Combine(uploadFolderPath, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                fileInfo.Add(CreateFileInfoObject(file, filePath));
            }

            return fileInfo;
        }

        private Model.Entities.FileInfo CreateFileInfoObject(IFormFile file, string filePath)
        {
            return new Model.Entities.FileInfo
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                FilePath = filePath,
                FileSize = ConvertFileSizeToMb(file.Length),
                fileExtension = Path.GetExtension(file.FileName)
            };
        }
    }
}
