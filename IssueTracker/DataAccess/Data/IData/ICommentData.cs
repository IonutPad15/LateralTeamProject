#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file
using DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Data.IData;

public interface ICommentData
{
    Task AddAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<IEnumerable<Comment?>> GetAllByCommentIdAsync(int id);
    Task<IEnumerable<Comment?>> GetAllByIssueIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllByUserIdAsync(Guid id);
    Task<Comment?> GetByIdAsync(int id);
    Task UpdateAsync(Comment comment);
}
