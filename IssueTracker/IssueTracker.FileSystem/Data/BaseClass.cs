using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem.Data;
public class BaseClass
{
    protected string ConnectionString { get; set; }
    protected string Container { get; set; } //TODO: aici o sa schimb treaba asta
    public BaseClass(IConfiguration config)
    {
        ConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;" +
            "QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        Container = "azureblobstorage";
    }
}
