using IssueTracker.FileSystem.Models;

namespace IssueTracker.FileSystem;

public interface IMetaDataProvider
{
    IEnumerable<MetaDataResp> GetAll();
    Task CreateAsync(MetaDataReq entity);
    Task<bool> DeleteAsync(string id, string group);
    Task<MetaDataResp?> GetAsync(string id, string group);
}
