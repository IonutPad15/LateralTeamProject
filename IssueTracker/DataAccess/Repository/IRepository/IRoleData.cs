using DataAccess.Models;

namespace DataAccess.Repository;

public interface IRoleData
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(int id);
}
