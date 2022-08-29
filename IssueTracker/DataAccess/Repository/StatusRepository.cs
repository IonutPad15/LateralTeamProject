using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;

public class StatusRepository : IStatusRepository
{
    private readonly ISQLDataAccess _db;
    public StatusRepository(ISQLDataAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Status>> GetAllAsync() =>
         await _db.LoadDataAsync<Status>("dbo.spStatus_GetAll");

    public async Task<Status?> GetByIdAsync(int id) =>
        (await _db.LoadDataAsync<Status, dynamic>("dbo.spStatus_Get", new { Id = id })).FirstOrDefault();
}
