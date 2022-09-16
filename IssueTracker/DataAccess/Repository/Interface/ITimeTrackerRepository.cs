using DataAccess.Models;

namespace DataAccess.Data.IData;
public interface ITimeTrackerRepository
{
    Task AddAsync(TimeTracker entity);
    Task DeleteAsync(int id);
}
