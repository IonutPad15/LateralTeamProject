using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;
public class HistoryRepository : IHistoryRepository
{
    private readonly ISQLDataAccess _db;
    public HistoryRepository(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<IEnumerable<History>> GetByProjectIdAsync(int projectId)
    {
        var history = await _db.LoadDataAsync<History, object>("spHistory_GetByProjeectId", new { ProjectId = projectId });
        return history;
    }
    public async Task<IEnumerable<History>> GetByIssueIdAsync(int issueId)
    {
        var history = await _db.LoadDataAsync<History, object>("spHistory_GetByIssueId", new { IssueId = issueId });
        return history;
    }
    public async Task<int> AddAsync(History entity)
    {
        var result = await _db.SaveDataAndGetIdAsync<object, int>("dbo.spHistory_Insert", new
        {
            Type = entity.Type,
            ProjectId = entity.ProjectId,
            UserId = entity.UserId,
            IssueId = entity.IssueId,
            ReferenceType = entity.ReferenceType,
            ReferenceId = entity.ReferenceId,
            Field = entity.ReferenceId,
            OldValue = entity.OldValue,
            NewValue = entity.NewValue,
            Updated = entity.NewValue
        });
        return result;
    }

    //I will delete this
}
