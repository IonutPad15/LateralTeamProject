using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace IssueTracker.FileSystem;
public class BolbData : IBolbData
{
    private readonly IBolbConfiguration _config;
    internal BolbData(IBolbConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<Models.FileModel>> GetFilesAsync(IEnumerable<Models.FileModel> files)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_config.ConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_config.Container);
        var fileResult = new List<Models.FileModel>();

        foreach (var file in files)
        {
            var fileAtachment = new Models.FileModel();
            fileAtachment.Name = file.Name;
            if (file.Name == null || file.Name == string.Empty)
                throw new ArgumentException("Invalid name file!");
            BlobClient information = containerClient.GetBlobClient(file.Name);
            if (information == null)
                throw new ArgumentException("Don't exist or was delete this file!");
            fileAtachment.Link = GetBlobSasUri(information).ToString();
            if (fileAtachment.Link == null)
                throw new ArgumentException($"Sas Invalid for {file.Name}!");
            fileResult.Add(fileAtachment);
        }
        return fileResult;
    }

    public async Task UploadFileAsync(Models.FileModel file)
    {
        if (file == null)
            throw new ArgumentException("You don't have files!");

        var containerClient = new BlobContainerClient(_config.ConnectionString, _config.Container);
        await containerClient.CreateIfNotExistsAsync();
        var blobClient = containerClient.GetBlobClient(file.Name);
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
}
