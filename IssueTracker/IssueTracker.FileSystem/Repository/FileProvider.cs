using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

[assembly: InternalsVisibleTo("IssueTracker.UnitTest")]
namespace IssueTracker.FileSystem;
internal class FileProvider : IFileProvider
{
    private readonly IMetaDataProvider _metadata;
    private readonly IBolbData _bolb;

    public FileProvider(IConfiguration config)
    {
        IConfigurationFactory cf = new ConfigurationFactory(config);
        var metaDataConfig = cf.Create<IMetaDataConfiguration>();
        _metadata = new MetaData(metaDataConfig);
        var blobConfig = cf.Create<IBolbConfigurationFactory>();
        _bolb = new BolbData(blobConfig);
    }

    public async Task<bool> DeleteAsync(Models.File file)
    {
        var result = await _metadata.DeleteAsync(file.Id, file.Extension);
        if (result == false) return false;
        result = await _bolb.DeleteFileAsync(file.Id + file.Extension);
        return result;
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
        await _bolb.UploadFileAsync(file);
        await _metadata.CreateAsync(file);
    }
}
