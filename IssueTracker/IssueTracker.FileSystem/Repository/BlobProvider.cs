using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace IssueTracker.FileSystem;
public class BlobProvider : IBlobProvider
{
    private readonly IBlobConfigurationFactory _config;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _containerClient;
    internal BlobProvider(IBlobConfigurationFactory config)
    {
        _config = config;
        _blobServiceClient = new BlobServiceClient(_config.ConnectionString);
        _containerClient = _blobServiceClient.GetBlobContainerClient(_config.Container);
    }

    public Task<IEnumerable<Models.File>> GetFilesAsync(IEnumerable<Models.File> files)
    {
        var fileResult = new List<Models.File>();
        if (files.Count() <= 0)
            throw new FileSystemException("I can't get nothing, files is empty!");
        foreach (var file in files)
        {
            if (string.IsNullOrEmpty(file.Id))
                throw new FileSystemException("Invalid file id!");
            if (string.IsNullOrEmpty(file.Extension))
                throw new FileSystemException("Invalid file extension!");
            var fileAtachment = new Models.File(file.Id, file.Extension);
            BlobClient information = _containerClient.GetBlobClient(file.Id + file.Extension);
            fileAtachment.Link = GetBlobSasUri(information).ToString();
            if (string.IsNullOrEmpty(fileAtachment.Link))
                throw new FileSystemException($"Sas Invalid for {file.Name}!");
            fileResult.Add(fileAtachment);
        }
        return Task.FromResult<IEnumerable<Models.File>>(fileResult);
    }

    public async Task UploadFileAsync(Models.File file)
    {
        if (file == null)
            throw new FileSystemException("You don't have files!");
        if (string.IsNullOrEmpty(file.BlobName))
            throw new FileSystemException("Invalid BlobName");
        if (file.Content == null)
            throw new FileSystemException("Invalid Content");
        var blobClient = _containerClient.GetBlobClient(file.BlobName);
        await blobClient.UploadAsync(file.Content);
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

    public async Task<bool> DeleteAsync(string name)
    {
        BlobClient toDelete = _containerClient.GetBlobClient(name);
        await toDelete.DeleteAsync();
        return true;
    }
}
