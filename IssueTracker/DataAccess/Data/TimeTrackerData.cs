using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;
public class TimeTrackerData : ITimeTrackerData
{
    private readonly ISQLDataAccess _db;
    public TimeTrackerData(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task Add(TimeTracker entity)
    {
        await _db.SaveDataAsync("spTimeTracker_Insert", new
        {
            Name = entity.Name,
            Description = entity.Description,
            Date = entity.Date,
            Worked = entity.Worked,
            Billable = entity.Billable,
            Remaining = entity.Remaining,
            UserId = entity.UserId,
            IssueId = entity.IssueId
        });
    }

    public async Task Delete(int id)
    {
        await _db.SaveDataAsync("spTimeTracker_Delete", new { Id = id });
    }
}
