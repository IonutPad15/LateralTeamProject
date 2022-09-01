using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;

public class PriorityRepository : IPriorityRepository
{
    private readonly ISQLDataAccess _db;
    public PriorityRepository(ISQLDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Priority>> GetAllAsync() =>
        await _db.LoadDataAsync<Priority>("dbo.spPriority_GetAll");

    public async Task<Priority?> GetByIdAsync(int id) =>
        (await _db.LoadDataAsync<Priority, dynamic>("dbo.spPriority_Get", new { Id = id })).FirstOrDefault();
}
