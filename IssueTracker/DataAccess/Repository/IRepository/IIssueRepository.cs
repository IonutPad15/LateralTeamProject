using DataAccess.Models;

namespace DataAccess.Repository;

public interface IIssueRepository
{
    Task<int> AddAsync(Issue entity);
    Task<IEnumerable<Issue>> GetAllAsync();
    Task<Issue?> GetByIdAsync(int id);
    Task UpdateAsync(Issue entity);
    Task DeleteAsync(int id);
    Task NextStatusOfIssueAsync(int id, int statusId);
    Task PreviousStatusOfIssueAsync(int id, int statusId);
    Task<int> GetProjectId(int id);
}
