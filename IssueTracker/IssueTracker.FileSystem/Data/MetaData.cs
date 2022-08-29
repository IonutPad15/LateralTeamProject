
using IssueTracker.FileSystem.Data.IData;
using Microsoft.Azure.Cosmos.Table;
using Models.Request;
using Models.Response;

namespace IssueTracker.FileSystem.Data;

public class MetaData : IMetaData
{
    private readonly CloudTable _metaDataTable;
    public MetaData()
    {
        var storageAccount = CloudStorageAccount.Parse("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
        var tableClient = storageAccount.CreateCloudTableClient();
        _metaDataTable = tableClient.GetTableReference("MetaData");
    }
    public IEnumerable<MetaDataResponse> GetAll()
    {
        var query = new TableQuery<MetaDataEntity>();
        var entities = _metaDataTable.ExecuteQuery(query);
        var models = entities.Select(x => new MetaDataResponse(x.RowKey, x.PartitionKey, x.Name, x.Type, x.SizeKb));
        return models;
    }
    public async Task CreateOrUpdateAsync(MetaDataRequest entity)
    {
        MetaDataEntity metaData = new MetaDataEntity();
        metaData.PartitionKey = entity.Group;
        metaData.RowKey = Guid.NewGuid().ToString();
        metaData.Name = entity.Name;
        metaData.Type = entity.Type;
        metaData.SizeKb = entity.SizeKb;
        var operation = TableOperation.InsertOrReplace(metaData);
        await _metaDataTable.ExecuteAsync(operation);
    }
    public async Task<bool> DeleteAsync(string id, string group)
    {
        var operation = TableOperation.Retrieve<MetaDataEntity>(group, id);
        var result = await _metaDataTable.ExecuteAsync(operation);
        var x = result.Result as MetaDataEntity;
        if (x != null)
        {
            operation = TableOperation.Delete(x);
            await _metaDataTable.ExecuteAsync(operation);
            return true;
        }
        return false;
    }
    public async Task<MetaDataResponse?> GetAsync(string rowKey, string partitionKey)
    {
        var operation = TableOperation.Retrieve<MetaDataEntity>(partitionKey, rowKey);
        var result = await _metaDataTable.ExecuteAsync(operation);
        var x = result.Result as MetaDataEntity;
        if (x != null)
        {
            var entity = new MetaDataResponse(x.RowKey, x.PartitionKey, x.Name, x.Type, x.SizeKb);
            return entity;
        }
        return null;
    }
}
public class MetaDataEntity : TableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double SizeKb { get; set; }
}
