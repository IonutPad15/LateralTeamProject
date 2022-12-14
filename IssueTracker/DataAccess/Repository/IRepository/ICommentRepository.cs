using DataAccess.Models;

namespace DataAccess.Repository;

public interface ICommentRepository
{
    Task<int> AddAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<IEnumerable<Comment?>> GetAllByCommentIdAsync(int id);
    Task<IEnumerable<Comment?>> GetAllByIssueIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllByUserIdAsync(Guid id);
    Task<Comment?> GetByIdAsync(int id);
    Task UpdateAsync(Comment comment);
}
