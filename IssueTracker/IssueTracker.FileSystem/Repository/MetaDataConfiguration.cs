
namespace IssueTracker.FileSystem;
public class MetaDataConfiguration : IMetaDataConfiguration
{
    public string ConnectionString { get; }
    public MetaDataConfiguration(string connstring)
    {
        ConnectionString = connstring;
    }
}
