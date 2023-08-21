namespace CORWL_API.Model.FetchDTO
{
    public record AzureBlobDto
    {
#nullable disable
        public string ConnectionString { get; set; }
        public string AccountKey { get; set; }
        public string Container { get; set; }
        public string AccountName { get; set; }

    };
}
