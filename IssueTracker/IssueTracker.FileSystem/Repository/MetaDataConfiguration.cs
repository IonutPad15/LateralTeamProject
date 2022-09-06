
namespace IssueTracker.FileSystem;
public class MetaDataConfiguration : IMetaDataConfiguration
{
    public string ConnectionString { get; }

    public string AzureTable { get; }

    public MetaDataConfiguration(string connstring, string azureTable)
    {
        ConnectionString = connstring;
        AzureTable = azureTable;
    }
}
