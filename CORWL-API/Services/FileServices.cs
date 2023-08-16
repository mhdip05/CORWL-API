using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CORWL_API.IServices;
using CORWL_API.Model.Entities;
using System.IO;

namespace CORWL_API.Services
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
            return fileName[(lastIndex + 1)..].ToLower();
        }

        private decimal ConvertFileSizeToMb(long fileSize)
        {

            var sizeToMb = fileSize / 1000000.0;
            string formatNumber = sizeToMb.ToString("0.00");
            return decimal.Parse(formatNumber);
        }

        public Model.Entities.FileInfo CreateFileInfoObject(IFormFile file, string filePath, string fileName)
        {
            return new Model.Entities.FileInfo
            {
                FileName = fileName,
                ContentType = file.ContentType,
                FilePath = filePath,
                FileSize = ConvertFileSizeToMb(file.Length),
                fileExtension = GetFileExtension(file.FileName),
                FileUnit = "MB",
                FileType = GetFileType(GetFileExtension(file.FileName)),
            };
        }

        public string ModifyFileName(string fileName)
        {
            int unixTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalHours;
            var nameOfFile = Path.GetFileNameWithoutExtension(fileName).Replace(" ","_");
            return (nameOfFile + "_" + Guid.NewGuid() + Path.GetExtension(fileName)).ToLower();
        }

        private string GetFileType(string fileExtension)
        {
            if (fileExtension == "jpg" || fileExtension == "jpeg" || fileExtension == "png")
                return "image";

            return "file";
        }

        private string BaseFolder()
        {
            return "FileUpload";
        }

        public async Task<List<Model.Entities.FileInfo>> CopyFileToServer(List<IFormFile> files, string directory, string subdirectory = null, string folderName = null)
        {

            var baseDirectoryPath = subdirectory == null
                ? Path.Combine(Directory.GetCurrentDirectory(), BaseFolder(), directory.ToLower())
                : Path.Combine(Directory.GetCurrentDirectory(), BaseFolder(), directory.ToLower(), subdirectory.ToLower());

            var uploadFolderPath = folderName == null
                 ? baseDirectoryPath
                 : Path.Combine(baseDirectoryPath, folderName);

            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            List<Model.Entities.FileInfo> fileInfo = new();

            foreach (var file in files)
            {
                var fileName = ModifyFileName(file.FileName);

                string filePath = Path.Combine(uploadFolderPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                fileInfo.Add(CreateFileInfoObject(file, filePath, fileName));
            }

            return fileInfo;
        }

        public bool DeleteFile(string directory, string fileName, string subdirectory = null)
        {
            var baseDirectory = _webHostEnvironment.WebRootPath;

            var basePath = subdirectory == null
                ? Path.Combine(baseDirectory, BaseFolder(), directory.ToLower())
    : Path.Combine(baseDirectory, _webHostEnvironment.WebRootPath, BaseFolder(), directory.ToLower(), subdirectory.ToLower());

            var fileToDelete = Path.Combine(basePath, fileName);

            if (!File.Exists(fileToDelete))
            {
                return false;
            }
            else
            {
                File.Delete(fileToDelete);
            }

            return true;
        }


    }
}


