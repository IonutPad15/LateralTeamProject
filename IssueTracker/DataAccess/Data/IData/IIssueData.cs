#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Data.IData;

public interface IIssueData
{
    Task AddAsync(Issue entity);
    Task<IEnumerable<Issue>> GetAllAsync();
    Task<Issue?> GetByIdAsync(int id);
    Task UpdateAsync(Issue entity);
    Task DeleteAsync(int id);
    Task NextStatusOfIssueAsync(int id, int statusId);
    Task PreviousStatusOfIssueAsync(int id, int statusId);
}
