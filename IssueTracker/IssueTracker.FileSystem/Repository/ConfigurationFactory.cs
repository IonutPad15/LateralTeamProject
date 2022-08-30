using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem;
public class ConfigurationFactory : IConfigurationFactory
{
    private readonly IConfiguration _config;
    public ConfigurationFactory(IConfiguration config)
    {
        _config = config;
    }
    public IConfigurationBase Create<T>() where T : IConfigurationBase
    {

        if (typeof(T) == typeof(IMetaDataConfiguration))
        {
            var connstring = _config.GetValue<string>("ConnectionStrings:Account");
            return new MetaDataConfiguration(connstring);

        }
        if (typeof(T) == typeof(IBolbConfiguration))
        {
            var connstring = _config.GetValue<string>("ConnectionStrings:Account");
            var container = _config.GetValue<string>("ConnectionStrings:Container");
            return new BlobConfiguration(connstring, container);
        }
        throw new InvalidOperationException();
    }

}
