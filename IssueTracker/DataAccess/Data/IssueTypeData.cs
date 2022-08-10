using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class IssueTypeData : IIssueTypeData
    {
        private readonly ISQLDataAccess _db;
        public IssueTypeData(ISQLDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<IssueType>> GetAllAsync() =>
           await _db.LoadDataAsync<IssueType>("dbo.spIssueType_GetAll");

        public async Task<IssueType?> GetByIdAsync(int id) =>
            (await _db.LoadDataAsync<IssueType, dynamic>("dbo.spIssueType_Get", new { Id = id })).FirstOrDefault();
    }
}
