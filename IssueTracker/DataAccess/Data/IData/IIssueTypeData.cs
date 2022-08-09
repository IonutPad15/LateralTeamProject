using DataAccess.Models;

namespace DataAccess.Data.IData
{
    public interface IIssueTypeData
    {
        Task<IEnumerable<IssueType>> GetAllAsync();
        Task<IssueType?> GetByIdAsync(int id);
    }
}
