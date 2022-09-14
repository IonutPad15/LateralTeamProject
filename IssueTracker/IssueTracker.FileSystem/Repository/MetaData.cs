using Microsoft.Azure.Cosmos.Table;

namespace IssueTracker.FileSystem;

public class MetaDataProvider : IMetaDataProvider
{
    private readonly CloudTable _metaDataTable;
    internal MetaDataProvider(IMetaDataConfiguration config)
    {
        var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
        var tableClient = storageAccount.CreateCloudTableClient();
        _metaDataTable = tableClient.GetTableReference(config.AzureTable);
        _metaDataTable.CreateIfNotExists();
    }
    public IEnumerable<Models.File> Get(IEnumerable<Models.File> files)
    {
        var query = new TableQuery<MetaDataEntity>();
        var entities = _metaDataTable.ExecuteQuery(query);
        var result = new List<Models.File>();
        foreach (var file in files)
        {
            var model = entities.FirstOrDefault(x => x.RowKey == file.BlobName && x.PartitionKey == file.Extension);
            if (model != null)
            {
                var resultFile = new Models.File(model.RowKey, model.PartitionKey, model.Name, model.Type, model.SizeKb);
                result.Add(resultFile);
            }
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
        var resultEntity = result.Result as MetaDataEntity;
        if (resultEntity != null)
        {
            operation = TableOperation.Delete(resultEntity);
            await _metaDataTable.ExecuteAsync(operation);
            return true;
        }
        return false;
    }
}
internal class MetaDataEntity : TableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double SizeKb { get; set; }
}
