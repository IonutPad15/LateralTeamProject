using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace IssueTracker.FileSystem;
public class BolbStorageProvider : IBolbStorageProvider
{
    private readonly IBolbConfigurationFactory _config;
    private BlobServiceClient BlobServiceClient { get; set; }
    private BlobContainerClient ContainerClient { get; set; }
    internal BolbStorageProvider(IBolbConfigurationFactory config)
    {
        _config = config;
        BlobServiceClient = new BlobServiceClient(_config.ConnectionString);
        ContainerClient = BlobServiceClient.GetBlobContainerClient(_config.Container);
    }

    public Task<IEnumerable<Models.File>> GetFilesAsync(IEnumerable<Models.File> files)
    {
        var fileResult = new List<Models.File>();
        if (files.Any())
            throw new ArgumentException("I can't get nothing, files is empty!");
        foreach (var file in files)
        {
            var fileAtachment = new Models.File();
            if (String.IsNullOrEmpty(file.Id))
                throw new ArgumentException("Invalid file id!");
            if (String.IsNullOrEmpty(file.Extension))
                throw new ArgumentException("Invalid file extension!");
            fileAtachment.Id = file.Id;
            fileAtachment.Extension = file.Extension;
            BlobClient information = ContainerClient.GetBlobClient(file.Id + file.Extension);
            fileAtachment.Link = GetBlobSasUri(information).ToString();
            if (fileAtachment.Link == null)
                throw new ArgumentException($"Sas Invalid for {file.Name}!");
            fileResult.Add(fileAtachment);
        }
        return Task.FromResult<IEnumerable<Models.File>>(fileResult);
    }

    public async Task UploadFileAsync(Models.File file)
    {
        if (file == null)
            throw new ArgumentException("You don't have files!");
        if (file.BlobName == String.Empty || file.BlobName == null)
            throw new ArgumentException("Invalid BlobName");
        if (file.Content == null)
            throw new ArgumentException("Invalid Content");
        var container = await ContainerClient.CreateIfNotExistsAsync();
        var blobClient = ContainerClient.GetBlobClient(file.BlobName);
        blobClient.Upload(file.Content);
    }

    private string GetBlobSasUri(BlobClient blobClient)
    {
        BlobSasBuilder sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = blobClient.BlobContainerName,
            BlobName = blobClient.Name,
            Resource = "b",
            StartsOn = DateTime.UtcNow.AddDays(-1),
            ExpiresOn = DateTime.UtcNow.AddDays(1),
        };
        sasBuilder.SetPermissions(BlobSasPermissions.Read);
        var sas = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(_config.AccountName, _config.AccountKey)).ToString();
        return $"{blobClient.Uri}?{sas}";
    }
}
