using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class StatusData : IStatusData
    {
        private readonly ISQLDataAccess _db;
        public StatusData(ISQLDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Status>> GetAllAsync() =>
             await _db.LoadDataAsync<Status>("dbo.spStatus_GetAll");

        public async Task<Status?> GetByIdAsync(int id) =>
            (await _db.LoadDataAsync<Status, dynamic>("dbo.spStatus_Get", new { Id = id })).FirstOrDefault();
    }
}
