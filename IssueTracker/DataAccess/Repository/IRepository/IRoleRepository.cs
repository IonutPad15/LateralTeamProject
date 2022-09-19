using DataAccess.Models;

namespace DataAccess.Repository;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(int id);
}
