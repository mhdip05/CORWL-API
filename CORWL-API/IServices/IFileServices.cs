namespace CORWL_API.IServices
{
    public interface IFileServices 
    {
#nullable disable
        Task<List<Model.Entities.FileInfo>> CopyFileToServer(List<IFormFile> file, string directory, string subdirectory = null, string folderName = null);
        bool DeleteFile(string directory, string fileName, string subdirectory=null);
    }
}
