namespace IssueTracker.FileSystem;
public interface IFileProvider
{
    Task UploadAsync(Models.File file);
    Task<IEnumerable<IssueTracker.FileSystem.Models.File>> GetAsync(IEnumerable<IssueTracker.FileSystem.Models.File> files);
    Task DeleteAsync(Models.File file);
}
