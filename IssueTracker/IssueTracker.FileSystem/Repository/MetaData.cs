using Microsoft.Azure.Cosmos.Table;
using IssueTracker.FileSystem.Models;

namespace IssueTracker.FileSystem;

public class MetaData : IMetaDataProvider
{
    private readonly CloudTable _metaDataTable;
    public MetaData(IMetaDataConfiguration config)
    {
        var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
        var tableClient = storageAccount.CreateCloudTableClient();
        _metaDataTable = tableClient.GetTableReference("MetaData");
    }
    public IEnumerable<MetaDataResp> GetAll()
    {
        var query = new TableQuery<MetaDataEntity>().Where(TableQuery.
            GenerateFilterConditionForBool(nameof(MetaDataEntity.IsDeleted),QueryComparisons.Equal,false));
        var entities = _metaDataTable.ExecuteQuery(query);
        var models = entities.Select(x => new MetaDataResp(x.RowKey, x.PartitionKey, x.Name, x.Type, x.SizeKb));
        if (models.Any())
            return models;
        return Enumerable.Empty<MetaDataResp>();
    }
    public async Task CreateAsync(MetaDataReq entity)
    {
        MetaDataEntity metaData = new MetaDataEntity();
        if (string.IsNullOrEmpty(entity.Group) || string.IsNullOrEmpty(entity.Id) ||
            string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Type)
            || entity.SizeKb <= 0)
            throw new ArgumentException();

        metaData.PartitionKey = entity.Group;
        metaData.RowKey = entity.Id;
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
            x.IsDeleted = true;
            operation = TableOperation.Replace(x);
            await _metaDataTable.ExecuteAsync(operation);
            return true;
        }
        return false;
    }
    public async Task<MetaDataResp?> GetAsync(string id, string group)
    {
        var operation = TableOperation.Retrieve<MetaDataEntity>(group, id);
        var result = await _metaDataTable.ExecuteAsync(operation);
        var x = result.Result as MetaDataEntity;
        if (x != null && x.IsDeleted == false)
        {
            var entity = new MetaDataResp(x.RowKey, x.PartitionKey, x.Name, x.Type, x.SizeKb);
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
    public bool IsDeleted { get; set; }
}
