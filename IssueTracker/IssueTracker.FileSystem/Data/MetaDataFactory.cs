using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem;
public class MetaDataFactory : IDataFactory
{
    private readonly IConfiguration _config;
    public MetaDataFactory(IConfiguration config)
    {
        _config = config;
    }
    public IMetaDataProvider CreateMetaData()
    {
        return new MetaData(_config);
    }
}
