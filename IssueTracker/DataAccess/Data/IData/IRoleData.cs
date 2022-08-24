using DataAccess.Models;

namespace DataAccess.Data.IData;

public interface IRoleData
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(int id);
}
