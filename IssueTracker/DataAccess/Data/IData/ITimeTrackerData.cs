using DataAccess.Models;

namespace DataAccess.Data.IData;
public interface ITimeTrackerData
{
    Task AddAsync(TimeTracker entity);
    Task DeleteAsync(int id);
}
