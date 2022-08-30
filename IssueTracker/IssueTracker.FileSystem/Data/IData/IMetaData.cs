using FileSystem.Models;
using Models.Response;

namespace FileSystem.Data.IData;

public interface IMetaData
{
    IEnumerable<MetaDataResp> GetAll();
    Task CreateAsync(MetaDataReq entity);
    Task<bool> DeleteAsync(string id, string group);
    Task<MetaDataResponse?> GetAsync(string rowKey, string partitionKey);
}
