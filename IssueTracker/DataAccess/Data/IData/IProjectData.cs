using DataAccess.Models;
using Models.Request;

namespace DataAccess.Data.IData
{
    public interface IProjectData
    {
        Task AddAsync(ProjectRequest project);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task UpdateAsync(ProjectRequest project);
        Task DeleteAsync(int id);
    }
}
