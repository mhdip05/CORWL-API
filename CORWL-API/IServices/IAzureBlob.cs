using Azure.Storage.Blobs.Models;

namespace CORWL_API.IServices
{
    public interface IAzureBlob
    {
        Task<List<Model.Entities.FileInfo>> UploadFileToAzureStorage(List<IFormFile> files, string directory, string subdirectory = "", string folderName = "");
        string CreateServiceSasForContainer();
        Task<bool> DeleteFileFromAzureStorage(string directory, string fileName);

    }
}
