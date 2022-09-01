using DataAccess.Models;

namespace DataAccess.Repository;
public interface IPriorityRepository
{
    Task<IEnumerable<Priority>> GetAllAsync();
    Task<Priority?> GetByIdAsync(int id);
}
