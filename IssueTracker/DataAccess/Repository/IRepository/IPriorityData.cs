using DataAccess.Models;

namespace DataAccess.Repository;
public interface IPriorityData
{
    Task<IEnumerable<Priority>> GetAllAsync();
    Task<Priority?> GetByIdAsync(int id);
}
