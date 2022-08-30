using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using FileSystem.Data.IData;
using Microsoft.Extensions.Configuration;

namespace FileSystem.Data;
public class BolbData : BaseClass, IBolbData
{
    public BolbData(IConfiguration config) : base(config)
    {
    }

    public String Get(string name)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
        string containerName = Container;
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        BlobClient x = containerClient.GetBlobClient(name);
        if (x == null) throw new ArgumentException("Don't exist or was delete this file!");
        return GetServiceSasUriForBlob(x).ToString(); //TODO: aici o sa imi livreze link-ul dupa verificarea sas
    }

    public void Upload(Stream file, string name)
    {
        if (file != null)
        {
            var containerClient = new BlobContainerClient(ConnectionString, Container);
            try
            {
                var blobClient = containerClient.GetBlobClient(name);
                blobClient.Upload(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        throw new ArgumentException("You don't have files!");
    }

    private static Uri GetServiceSasUriForBlob(BlobClient blobClient,
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
                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                sasBuilder.SetPermissions(BlobSasPermissions.Read |
                    BlobSasPermissions.Write);
            }
            else
            {
                sasBuilder.Identifier = storedPolicyName;
            }

            Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
            Console.WriteLine("SAS URI for blob is: {0}", sasUri);
            Console.WriteLine();

            return sasUri;
        }
        throw new ArgumentException("BlobContainerClient must be authorized with Shared Key credentials to create a service SAS.");
    }
}
