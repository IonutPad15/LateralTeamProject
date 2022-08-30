using DataAccess.Models;

namespace DataAccess.Repository;

public interface IProjectData
{
    Task<int> AddAsync(Project project);
    Task<IEnumerable<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task UpdateAsync(Project project);
    Task DeleteAsync(int id);
}
