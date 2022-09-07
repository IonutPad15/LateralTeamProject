using Azure.Storage.Blobs;
using Azure.Storage.Sas;


namespace IssueTracker.FileSystem;
public class BolbData : IBolbData
{
    private readonly IBolbConfigurationFactory _config;
    private BlobServiceClient BlobServiceClient { get; set; }
    private BlobContainerClient ContainerClient { get; set; }
    internal BolbData(IBolbConfigurationFactory config)
    {
        _config = config;
        BlobServiceClient = new BlobServiceClient(_config.ConnectionString);
        ContainerClient = BlobServiceClient.GetBlobContainerClient(_config.Container);
    }

    public Task<IEnumerable<Models.File>> GetFilesAsync(IEnumerable<Models.File> files)
    {
        var fileResult = new List<Models.File>();

        foreach (var file in files)
        {
            var fileAtachment = new Models.File();
            if (file.Id == null || file.Id == string.Empty)
                throw new ArgumentException("Invalid file id!");
            fileAtachment.Id = file.Id;
            fileAtachment.Extension = file.Extension;
            BlobClient information = ContainerClient.GetBlobClient(file.Id + file.Extension);
            if (information == null)
                throw new ArgumentException("Don't exist or was delete this file!");
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

        await ContainerClient.CreateIfNotExistsAsync();
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

    public async Task<Stream> DownloadFileAsync(string link)
    {
        Uri sasUri = new Uri(link);
        BlobClient blobClient = new BlobClient(sasUri, null);
        if (blobClient == null)
            throw new ArgumentException("This file can't be find!");
        var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        if (memoryStream.Length <= 0)
            throw new ArgumentException("This file can't be downloading!");
        return memoryStream;
    }
    public async Task<bool> DeleteFileAsync(string name)
    {
        BlobClient toDelete = ContainerClient.GetBlobClient(name);
        await toDelete.DeleteAsync();
        return true;
    }
}
