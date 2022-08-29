using DataAccess.Models;

namespace DataAccess.Repository;

public interface IIssueTypeRepository
{
    Task<IEnumerable<IssueType>> GetAllAsync();
    Task<IssueType?> GetByIdAsync(int id);
}
