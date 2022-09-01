namespace IssueTracker.FileSystem;
public class BlobConfiguration : IBolbConfiguration
{
    public string Container { get; }

    public string ConnectionString { get; }
    public BlobConfiguration(string connstring, string container)
    {
        Container = container;
        ConnectionString = connstring;
    }
}
