using IssueTracker.FileSystem.Models;
using IssueTracker.FileSystem.Repository.IRepository;
using Microsoft.Extensions.Configuration;

namespace IssueTracker.FileSystem.Repository;
public class FileProvider : IFileProvider
{
    private readonly IMetaDataProvider _metadata;
    private readonly IBolbData _bolb;

    internal FileProvider(IConfiguration config, IMetaDataProvider metadata, IBolbData bolb)
    {
        IConfigurationFactory dataFactory = new ConfigurationFactory(config);
        IMetaDataConfiguration metaDataConfig = (IMetaDataConfiguration)dataFactory.Create<IMetaDataConfiguration>();
        IBolbConfiguration bolbConfig = (IBolbConfiguration)dataFactory.Create<IBolbConfiguration>();
        _metadata = metadata;
        _bolb = bolb;
    }

    public Task GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(FileModel file)
    {
        throw new NotImplementedException();
    }

    public async Task UploadAsync(FileModel file)
    {
        file.Id = Guid.NewGuid().ToString();
        await _bolb.UploadFileAsync(file);
        await _metadata.CreateAsync(file);
    }
}
