using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace IssueTracker.FileSystem;
public class BolbData : IBolbData
{
    private readonly IBolbConfiguration _config;
    public BolbData(IBolbConfiguration config)
    {
        _config = config;
    }


    public String Get(string fileName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_config.ConnectionString);
        string containerName = _config.Container;

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient x = containerClient.GetBlobClient(name);
        if (x == null) throw new ArgumentException("Don't exist or was delete this file!");
        return GetBlobSasUri(x).ToString(); //TODO: aici o sa imi livreze link-ul dupa verificarea sas
    }

    public void Upload(Stream file, string name)
    {
        if (file != null)
        {
            var containerClient = new BlobContainerClient(_config.ConnectionString, _config.Container);
            try
            {
                var blobClient = containerClient.GetBlobClient(name);
                blobClient.Upload(file);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        throw new ArgumentException("You don't have files!");
    }

    private static string GetBlobSasUri(BlobClient blobClient)
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

        //TODO: move AccountName and AccountKey into configuration
        var sas = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential("devstoreaccount1", "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==")).ToString();
        return $"{blobClient.Uri}?{sas}";
    }
}
