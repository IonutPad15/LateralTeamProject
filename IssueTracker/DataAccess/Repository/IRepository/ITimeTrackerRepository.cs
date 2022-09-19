using DataAccess.Models;

namespace DataAccess.Repository.IRepository;
public interface ITimeTrackerRepository
{
    Task AddAsync(TimeTracker entity);
    Task DeleteAsync(int id);
}
