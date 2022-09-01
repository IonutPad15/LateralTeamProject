using IssueTracker.FileSystem.Models;

namespace IssueTracker.FileSystem.Repository.IRepository;
public interface IFileProvider
{
    Task UploadAsync(FileModel file);
    Task GetAsync();
    Task DeleteAsync(FileModel file);
}
