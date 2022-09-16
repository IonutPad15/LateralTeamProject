using DataAccess.Models;

namespace DataAccess.Repository;
public interface IHistoryRepository
{
    Task<int> AddAsync(History entity);
    Task<IEnumerable<History>> GetByIssueIdAsync(int issueId);
    Task<IEnumerable<History>> GetByProjectIdAsync(int projectId);
    //I will delete this
}
