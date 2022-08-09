using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class RoleData : IRoleData
    {
        private readonly ISQLDataAccess _db;
        public RoleData(ISQLDataAccess db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Role>> GetAllAsync() =>
            await _db.LoadData<Role>("dbo.spRole_GetAll");

        public async Task<Role?> GetByIdAsync(int id) =>
            (await _db.LoadData<Role, dynamic>("dbo.spRole_Get", new { Id = id })).FirstOrDefault();
    }
}
