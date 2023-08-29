namespace CORWL_API.IServices
{
    public interface IAzureBlob
    {
        Task<List<Model.Entities.FileInfo>> UploadFileToAzureStorage(List<IFormFile> files, string directory, string subdirectory = "", string folderName = "");
        string TestCredential();
        Task<bool> DeleteFileFromAzureStorage(string directory, string fileName);

    }
}
