using IssueTracker.FileSystem.Models;

namespace IssueTracker.FileSystem;

internal interface IMetaDataProvider
{
    IEnumerable<MetaDataResponse> GetAll();
    Task CreateAsync(FileModel entity); //I change MetadataRequest in FileModel
    Task<bool> DeleteAsync(string id, string group);
    Task<MetaDataResponse?> GetAsync(string id, string group);
}
