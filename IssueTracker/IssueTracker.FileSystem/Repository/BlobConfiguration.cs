namespace IssueTracker.FileSystem;
public class BlobConfiguration : IBlobConfigurationFactory
{
    public BlobConfiguration(string container, string connstring, string accountName, string accountKey)
    {
        Container = container;
        ConnectionString = connstring;
        AccountName = accountName;
        AccountKey = accountKey;
    }
    public string Container { get; }
    public string ConnectionString { get; }
    public string AccountName { get; }
    public string AccountKey { get; }
}
