using DataAccess.Models;

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
