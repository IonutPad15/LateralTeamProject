using IssueTracker.FileSystem.Models;

namespace IssueTracker.FileSystem;

public interface IMetaDataProvider
{
    IEnumerable<MetaDataResponse> GetAll();
    Task CreateAsync(MetaDataRequest entity);
    Task<bool> DeleteAsync(string id, string group);
    Task<MetaDataResponse?> GetAsync(string id, string group);
}
