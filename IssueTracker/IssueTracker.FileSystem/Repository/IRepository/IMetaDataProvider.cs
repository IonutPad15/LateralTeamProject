using IssueTracker.FileSystem.Models;

namespace IssueTracker.FileSystem;

internal interface IMetaDataProvider
{
    IEnumerable<FileModel> GetAll(IEnumerable<FileModel> files);
    Task CreateAsync(FileModel entity); //I change MetadataRequest in FileModel
    Task<bool> DeleteAsync(string id, string group);
    Task<MetaDataResponse?> GetAsync(string id, string group);
}
