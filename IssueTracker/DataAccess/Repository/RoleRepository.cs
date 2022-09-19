using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly ISQLDataAccess _db;
    public RoleRepository(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<IEnumerable<Role>> GetAllAsync() =>
        await _db.LoadDataAsync<Role>("dbo.spRole_GetAll");

    public async Task<Role?> GetByIdAsync(int id) =>
        (await _db.LoadDataAsync<Role, dynamic>("dbo.spRole_Get", new { Id = id })).FirstOrDefault();
}
