namespace IssueTracker.FileSystem;
public class BlobConfiguration : IBolbConfiguration
{
    public string Container { get; }

    public string ConnectionString { get; }
    public BlobConfiguration(string container, string connstring)
    {
        Container = container;
        ConnectionString = connstring;
    }
}
