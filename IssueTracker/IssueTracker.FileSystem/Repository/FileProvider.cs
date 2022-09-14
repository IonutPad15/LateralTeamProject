using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("IssueTracker.UnitTest")]
namespace IssueTracker.FileSystem;
public class FileProvider : IFileProvider
{
    private readonly IMetaDataProvider _metaDataProvider;
    private readonly IBlobProvider _bolbProvider;

    public FileProvider(IConfiguration config)
    {
        IConfigurationFactory cf = new ConfigurationFactory(config);
        var metaDataConfig = cf.Create<IMetaDataConfiguration>();
        _metaDataProvider = new MetaDataProvider(metaDataConfig);
        var blobConfig = cf.Create<IBlobConfigurationFactory>();
        _bolbProvider = new BlobData(blobConfig);
    }

    public async Task<bool> DeleteAsync(Models.File file)
    {
        var result = await _metaDataProvider.DeleteAsync(file.Id, file.Extension);
        if (result == false) return false;
        result = await _bolbProvider.DeleteAsync(file.Id + file.Extension);
        return result;
    }

    public async Task<IEnumerable<Models.File>> GetAsync(IEnumerable<Models.File> files)
    {
        var entities = _metaDataProvider.Get(files);
        if (!entities.Any()) throw new ArgumentException("Detail file is null!");
        var bolb = await _bolbProvider.GetFilesAsync(files);
        if (!entities.Any()) throw new ArgumentException("File is null!");
        foreach (var entity in entities)
        {
            var link = bolb.Where(b => b.Id == entity.Id).FirstOrDefault()?.Link;
            if (link == null || link == String.Empty)
                throw new ArgumentException("Link null or empty");
            entity.Link = link;
            var extension = bolb.FirstOrDefault(b => b.Id == entity.Id)?.Extension;
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
        await _bolbProvider.UploadFileAsync(file);
        await _metaDataProvider.CreateAsync(file);
    }
}
