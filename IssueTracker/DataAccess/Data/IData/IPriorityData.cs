using DataAccess.Models;

namespace DataAccess.Data.IData;
public interface IPriorityData
{
    Task<IEnumerable<Priority>> GetAllAsync();
    Task<Priority?> GetByIdAsync(int id);
}
