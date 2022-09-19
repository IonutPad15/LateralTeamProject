namespace IssueTracker.FileSystem;
internal interface IMetaDataProvider
{
    IEnumerable<Models.File> Get(IEnumerable<Models.File> files);
    Task CreateAsync(Models.File entity);
    Task<bool> DeleteAsync(string id, string group);
}
