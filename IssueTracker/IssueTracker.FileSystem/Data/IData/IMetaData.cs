using Models.Request;
using Models.Response;

namespace IssueTracker.FileSystem.Data.IData;

public interface IMetaData
{
    IEnumerable<MetaDataResponse> GetAll();
    Task CreateOrUpdateAsync(MetaDataRequest entity);
    Task<bool> DeleteAsync(string id, string group);
    Task<MetaDataResponse?> GetAsync(string rowKey, string partitionKey);
}
