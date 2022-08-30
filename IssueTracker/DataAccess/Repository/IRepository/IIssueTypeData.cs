using DataAccess.Models;

namespace DataAccess.Repository;

public interface IIssueTypeData
{
    Task<IEnumerable<IssueType>> GetAllAsync();
    Task<IssueType?> GetByIdAsync(int id);
}
