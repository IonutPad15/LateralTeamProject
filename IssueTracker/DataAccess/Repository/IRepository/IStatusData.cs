using DataAccess.Models;

namespace DataAccess.Repository;

public interface IStatusData
{
    Task<IEnumerable<Status>> GetAllAsync();
    Task<Status?> GetByIdAsync(int id);
}
