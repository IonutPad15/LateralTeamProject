using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IssueTracker.UnitTest")]
namespace IssueTracker.FileSystem;
internal interface IBolbData
{
    Task<IEnumerable<Models.File>> GetFilesAsync(IEnumerable<Models.File> files);
    Task UploadFileAsync(Models.File file);
}
