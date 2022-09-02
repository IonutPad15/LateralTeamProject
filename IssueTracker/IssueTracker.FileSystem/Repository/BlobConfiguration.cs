namespace IssueTracker.FileSystem;
public class BlobConfiguration : IBolbConfigurationFactory
{
    public string Container { get; }
    public string ConnectionString { get; }
    public string AccountName { get; }
    public string AccountKey { get; }
    public BlobConfiguration(string container, string connstring, string accountName, string accountKey)
    {
        Container = container;
        ConnectionString = connstring;
        AccountName = accountName;
        AccountKey = accountKey;
    }
}
