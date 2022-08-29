using DataAccess.Models;

namespace DataAccess.Data.IData;

public interface IProjectData
{
    Task<int> AddAsync(Project project);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task UpdateAsync(Project project);
    Task DeleteAsync(int id);
}
