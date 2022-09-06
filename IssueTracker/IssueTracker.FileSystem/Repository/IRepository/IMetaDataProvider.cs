using System.Runtime.CompilerServices;
using IssueTracker.FileSystem.Models;

[assembly: InternalsVisibleTo("IssueTracker.UnitTest")]
namespace IssueTracker.FileSystem;

internal interface IMetaDataProvider
{
    IEnumerable<Models.File> GetAll(IEnumerable<Models.File> files);
    Task CreateAsync(Models.File entity); //I change MetadataRequest in FileModel
    Task<bool> DeleteAsync(string id, string group);
    Task<MetaDataResponse?> GetAsync(string id, string group);
}
