using IssueTracker.FileSystem.Models;

namespace IssueTracker.FileSystem.Repository.IRepository;
public interface IFileProvider
{
    Task UploadAsync(FileModel file);
    Task<IEnumerable<IssueTracker.FileSystem.Models.FileModel>> GetAsync(IEnumerable<IssueTracker.FileSystem.Models.FileModel> files);
    Task DeleteAsync(FileModel file);
}
