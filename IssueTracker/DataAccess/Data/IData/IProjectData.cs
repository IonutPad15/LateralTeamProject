using DataAccess.Models;
using Models.Request;

namespace DataAccess.Data.IData
{
    public interface IProjectData
    {
        Task AddAsync(Project project);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}
