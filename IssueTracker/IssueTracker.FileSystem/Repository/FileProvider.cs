using IssueTracker.FileSystem.Models;
using IssueTracker.FileSystem.Repository.IRepository;
using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem.Repository;
public class FileProvider : IFileProvider
{
    private readonly IMetaDataProvider _metadata;
    private readonly IBolbData _bolb;

    public static FileProvider GetFileProvider(IConfiguration config)
    {
        IConfigurationFactory cf = new ConfigurationFactory(config);
        var metaDataConfig = (IMetaDataConfiguration)cf.Create<IMetaDataConfiguration>();
        IMetaDataProvider metaDataProvider = new MetaData(metaDataConfig);
        var blobConfig = (IBolbConfigurationFactory)cf.Create<IBolbConfigurationFactory>();
        IBolbData blobData = new BolbData(blobConfig);
        FileProvider fileProvider = new FileProvider(config, metaDataProvider, blobData);
        return fileProvider;
    }
    public FileProvider(IConfiguration config)
    {
        IConfigurationFactory cf = new ConfigurationFactory(config);
        var metaDataConfig = (IMetaDataConfiguration)cf.Create<IMetaDataConfiguration>();
        _metadata = new MetaData(metaDataConfig);
        var blobConfig = (IBolbConfigurationFactory)cf.Create<IBolbConfigurationFactory>();
        _bolb = new BolbData(blobConfig);
    }
    internal FileProvider(IConfiguration config, IMetaDataProvider metadata, IBolbData bolb)
    {
        IConfigurationFactory dataFactory = new ConfigurationFactory(config);
        IMetaDataConfiguration metaDataConfig = (IMetaDataConfiguration)dataFactory.Create<IMetaDataConfiguration>();
        IBolbConfigurationFactory bolbConfig = (IBolbConfigurationFactory)dataFactory.Create<IBolbConfigurationFactory>();
        _metadata = metadata;
        _bolb = bolb;
    }

    public async Task DeleteAsync(Models.File file)
    {
        await _metadata.DeleteAsync(file.Id, file.Extension);
    }

    public async Task<IEnumerable<Models.File>> GetAsync(IEnumerable<Models.File> files)
    {
        var entities = _metadata.GetAll(files);
        if (!entities.Any()) throw new ArgumentException("Detail file is null!");
        var bolb = await _bolb.GetFilesAsync(files);
        if (!entities.Any()) throw new ArgumentException("File is null!");
        foreach (var entity in entities)
        {
            var link = bolb.Where(b => b.Id == entity.Id).FirstOrDefault()?.Link;
            if (link == null || link == String.Empty)
                throw new ArgumentException("Link null or empty");
            entity.Link = link;
            var extension = bolb.Where(b => b.Id == entity.Id).FirstOrDefault()?.Extension;
            if (extension == null || extension == String.Empty)
                throw new ArgumentException("Extension null or empty");
            entity.Extension = extension;
        }
        return entities;
    }

    public async Task UploadAsync(Models.File file)
    {
        if (file == null)
            throw new ArgumentException("File is null!");
        file.Id = Guid.NewGuid().ToString();
        await _bolb.UploadFileAsync(file);
        await _metadata.CreateAsync(file);
    }
}
