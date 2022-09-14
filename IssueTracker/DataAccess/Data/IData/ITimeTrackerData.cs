using DataAccess.Models;

namespace DataAccess.Data.IData;
public interface ITimeTrackerData
{
    Task Add(TimeTracker entity);
    Task Delete(int id);
}
