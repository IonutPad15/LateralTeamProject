using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository;
public class TimeTrackerRepository : ITimeTrackerRepository
{
    private readonly ISQLDataAccess _db;
    public TimeTrackerRepository(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task AddAsync(TimeTracker entity)
    {
        if (IsValid(entity))
            throw new ArgumentException("TimeTracker is invalid!");
        await _db.SaveDataAsync("spTimeTracker_Insert", new
        {
            Name = entity.Name,
            Description = entity.Description,
            Date = entity.Date,
            //Worked = entity.Worked,
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
    private bool IsValid(TimeTracker entity)
    {
        if (String.IsNullOrEmpty(entity.Name))
            return false;
        if (entity.Worked == TimeSpan.Zero || entity.Billable == TimeSpan.Zero)
            return false;
        if (entity.UserId == Guid.Empty)
            return false;
        if (entity.IssueId <= 0)
            return false;
        return true;
    }
}
