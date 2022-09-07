using Microsoft.Azure.Cosmos.Table;
using IssueTracker.FileSystem.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IssueTracker.UnitTest")]
namespace IssueTracker.FileSystem;

public class MetaData : IMetaDataProvider
{
    private readonly CloudTable _metaDataTable;
    internal MetaData(IMetaDataConfiguration config)
    {
        var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
        var tableClient = storageAccount.CreateCloudTableClient();
        _metaDataTable = tableClient.GetTableReference(config.AzureTable);
        _metaDataTable.CreateIfNotExists();
    }
    public IEnumerable<Models.File> GetAll(IEnumerable<Models.File> files)
    {
        var query = new TableQuery<MetaDataEntity>().Where(TableQuery.
            GenerateFilterConditionForBool(nameof(MetaDataEntity.IsDeleted), QueryComparisons.Equal, false));
        var entities = _metaDataTable.ExecuteQuery(query);
        var result = new List<Models.File>();
        foreach (var file in files)
        {
            var models = entities.Where(x => x.RowKey == file.Id).Select(x => new Models.File(x.RowKey, x.PartitionKey, x.Name, x.Type, x.SizeKb)).FirstOrDefault();
            if (models == null) throw new ArgumentException("I can't find it!");
            result.Add(models);
        }
        if (result.Any())
            return result;
        return Enumerable.Empty<Models.File>();
    }
    public async Task CreateAsync(Models.File entity)
    {
        MetaDataEntity metaData = new MetaDataEntity();
        if (string.IsNullOrEmpty(entity.Extension) || string.IsNullOrEmpty(entity.Id) ||
            string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Type)
            || entity.SizeKb <= 0)
            throw new ArgumentException();

        metaData.PartitionKey = entity.Extension;
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
    public async Task<MetaDataResponse?> GetAsync(string id, string group)
    {
        var operation = TableOperation.Retrieve<MetaDataEntity>(group, id);
        var result = await _metaDataTable.ExecuteAsync(operation);
        var x = result.Result as MetaDataEntity;
        if (x != null && x.IsDeleted == false)
        {
            var entity = new MetaDataResponse(x.RowKey, x.PartitionKey, x.Name, x.Type, x.SizeKb);
            return entity;
        }
        return null;
    }
}
internal class MetaDataEntity : TableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double SizeKb { get; set; }
    public bool IsDeleted { get; set; }
}
