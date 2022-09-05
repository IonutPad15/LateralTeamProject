namespace DataAccess.Repository;
public interface IFileRepository
{
    Task<string?> AddAsync(Models.File entity);
    Task<IEnumerable<Models.File>> GetByIssueIdAsync(int issueId);
    Task DeleteAsync(string fileId);
}
