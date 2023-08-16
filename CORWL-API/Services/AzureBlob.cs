using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using CORWL_API.IServices;
using CORWL_API.Model.FetchDTO;
using Microsoft.Extensions.Options;

namespace CORWL_API.Services
{
    public class AzureBlob : IAzureBlob
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly IFileServices _fileServices;
        private readonly IOptions<AzureBlobDto> _azureOptions;

        public AzureBlob(IFileServices fileServices, IOptions<AzureBlobDto> azureOptions)
        {
            _blobServiceClient = new BlobServiceClient(azureOptions.Value.ConnectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(azureOptions.Value.Container);
            _fileServices = fileServices;
            _azureOptions = azureOptions;

        }

        public string CreateServiceSasForContainer()
        {
            BlobSasBuilder sasBuilder = new()
            {
                BlobContainerName = _azureOptions.Value.Container,
                Resource = "c",
            };

            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read | BlobContainerSasPermissions.List);
            sasBuilder.StartsOn = DateTime.UtcNow;
            sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(20);

            return sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(_azureOptions.Value.AccountName, _azureOptions.Value.AccountKey)).ToString();
        }


        public async Task<List<Model.Entities.FileInfo>> UploadFileToAzureStorage(List<IFormFile> files, string directory, string subdirectory, string folderName)
        {
            var fileInfo = new List<Model.Entities.FileInfo>();
            var localFilePath = Path.Combine(directory, subdirectory, folderName);

            foreach (var item in files)
            {
                var fileName = _fileServices.ModifyFileName(item.FileName);
                var blobClient = _containerClient.GetBlobClient(localFilePath + "/" + fileName);

                using Stream stream = item.OpenReadStream();
                var a = await blobClient.UploadAsync(stream);

                fileInfo.Add(_fileServices.CreateFileInfoObject(item, localFilePath, fileName));
            }

            return fileInfo;
        }

        public async Task<bool> DeleteFileFromAzureStorage(string directory, string fileName)
        {
            var blobClient = _containerClient.GetBlobClient($"{directory}/{fileName}");

            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots);
                return true;
            }
            return false;
        }
    }

}
