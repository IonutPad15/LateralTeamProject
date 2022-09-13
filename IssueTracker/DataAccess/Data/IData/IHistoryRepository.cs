using DataAccess.Models;

namespace DataAccess.Data.IData;
public interface IHistoryRepository
{
    Task<int> AddAsync(History entity);
    Task<IEnumerable<History>> GetByIssueIdAsync(int issueId);
    Task<IEnumerable<History>> GetByProjectIdAsync(int projectId);
}
