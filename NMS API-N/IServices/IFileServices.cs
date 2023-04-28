namespace NMS_API_N.IServices
{
    public interface IFileServices 
    {
#nullable disable
        public Task<List<Model.Entities.FileInfo>> CopyFileToServer(List<IFormFile> file, string directory, string subdirectory = null, string folderName = null);
    }
}
