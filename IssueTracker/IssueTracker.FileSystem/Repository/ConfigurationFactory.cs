﻿using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem;
internal class ConfigurationFactory : IConfigurationFactory
{
    private readonly IConfiguration _config;
    internal ConfigurationFactory(IConfiguration config)
    {
        _config = config;
    }
    public T Create<T>() where T : IConfigurationBase
    {
        if (typeof(T) == typeof(IMetaDataConfiguration))
        {
            var connstring = _config.GetValue<string>("ConnectionStrings:Account");
            return (T)(new MetaDataConfiguration(connstring) as IMetaDataConfiguration);

        }
        if (typeof(T) == typeof(IBolbConfigurationFactory))
        {
            var connstring = _config.GetValue<string>("ConnectionStrings:Account");
            var container = _config.GetValue<string>("ConnectionStrings:Container");
            var accountName = _config.GetValue<string>("ConnectionStrings:AccountName");
            var accountKey = _config.GetValue<string>("ConnectionStrings:AccountKey");
            return (T)(new BlobConfiguration(container, connstring, accountName, accountKey) as IBolbConfigurationFactory);
        }
        throw new InvalidOperationException();
    }

}
