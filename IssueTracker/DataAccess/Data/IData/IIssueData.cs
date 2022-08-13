using DataAccess.Models;

namespace DataAccess.Data.IData
{
    public interface IIssueData
    {
        Task AddAsync(Issue entity);
        Task<IEnumerable<Issue>> GetAllAsync();
        Task<Issue?> GetByIdAsync(int id);
        Task UpdateAsync(Issue entity);
        Task DeleteAsync(int id);
        Task NextPreview(int id, string method = "next");
    }
}
