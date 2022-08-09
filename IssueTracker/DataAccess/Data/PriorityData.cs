using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class PriorityData : IPriorityData
    {
        private readonly ISQLDataAccess _db;
        public PriorityData(ISQLDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Priority>> GetAllAsync() =>
            await _db.LoadData<Priority, dynamic>("dbo.spPriority_GetAll", new {});

        public async Task<Priority?> GetByIdAsync(int id) =>
            (await _db.LoadData<Priority, dynamic>("dbo.spPriority_Get", new { Id = id })).FirstOrDefault();
    }
}
