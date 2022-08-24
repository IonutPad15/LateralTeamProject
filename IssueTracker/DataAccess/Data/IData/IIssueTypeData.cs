#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Data.IData;

public interface IIssueTypeData
{
    Task<IEnumerable<IssueType>> GetAllAsync();
    Task<IssueType?> GetByIdAsync(int id);
}
