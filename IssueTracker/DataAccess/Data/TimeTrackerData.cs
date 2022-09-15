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
    public async Task AddAsync(TimeTracker entity)
    {
        if (String.IsNullOrEmpty(entity.Name))
            throw new ArgumentException("Name invalid");
        if (entity.Worked == TimeSpan.Zero || entity.Billable == TimeSpan.Zero)
            throw new ArgumentException("Invalid Worked or Billable");
        if (entity.UserId == Guid.Empty)
            throw new ArgumentException("Invalid User!");
        if (entity.IssueId <= 0)
            throw new ArgumentException("Invalid Issue!");
        await _db.SaveDataAsync("spTimeTracker_Insert", new
        {
            Name = entity.Name,
            Description = entity.Description,
            Date = entity.Date,
            Worked = entity.Worked,
            Billable = entity.Billable,
            UserId = entity.UserId,
            IssueId = entity.IssueId
        });
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id is invalid");
        await _db.SaveDataAsync("spTimeTracker_Delete", new { Id = id });
    }
}
