using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
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
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_config.Container);
        if (!containerClient.Exists())
        {
            CreateBlob(_config.Container);
        }
        BlobClient information = containerClient.GetBlobClient(fileName);
        if (information == null) throw new ArgumentException("Don't exist or was delete this file!");
        return GetServiceSasUriForBlob(information).ToString(); //TODO: aici o sa imi livreze link-ul dupa verificarea sas
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

    private static string GetServiceSasUriForBlob(BlobClient blobClient,
     string storedPolicyName = "") //TODO: am de verificat sas.
    {
        if (blobClient.CanGenerateSasUri)
        {
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                BlobName = blobClient.Name,
                Resource = "b"
            };

            if (storedPolicyName == "")
            {
                sasBuilder.StartsOn = DateTime.UtcNow.AddMinutes(-15);
                sasBuilder.ExpiresOn = DateTime.UtcNow.AddMonths(1);
                sasBuilder.SetPermissions(BlobSasPermissions.Read |
                    BlobSasPermissions.Write);
            }
            else
            {
                sasBuilder.Identifier = storedPolicyName;
            }
            var sasToken = sasBuilder.ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential("devstoreaccount1", "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==")).ToString();
            Console.WriteLine("SAS URI for blob is: {0}", sasToken);
            Console.WriteLine();
            return sasToken;
        }
        throw new ArgumentException("BlobContainerClient must be authorized with Shared Key credentials to create a service SAS.");
    }

    public void CreateBlob(string name)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_config.ConnectionString);
        try
        {
            BlobContainerClient containerClient = blobServiceClient.CreateBlobContainer(name);
        }
        catch
        {
            throw new ArgumentException("This container already exist!");
        }
    }
}
