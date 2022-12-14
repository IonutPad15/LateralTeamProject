namespace DataAccess.Repository;
public interface IFileRepository
{
    Task<string?> AddAsync(Models.File entity);
    Task<IEnumerable<Models.File>> GetByIssueIdAsync(int issueId);
    Task<Models.File?> GetAsync(string fileId);
    Task DeleteAsync(string fileId);
    Task<IEnumerable<Models.File>> GetForCleanupAsync(TimeSpan timeSpan);
}
