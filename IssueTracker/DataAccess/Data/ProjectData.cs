using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class ProjectData : IProjectData
    {
        private readonly ISQLDataAccess _db;
        public ProjectData(ISQLDataAccess db)
        {
            _db= db;
        }

        public async Task AddAsync(Project project) =>
            await _db.SaveDataAsync("dbo.spProject_Insert", new { project.Title, project.Description, DateTime.UtcNow});

        public async Task<IEnumerable<Project>> GetAllAsync() =>
            await _db.LoadDataAsync<Project>("dbo.spProject_GetAll");

        public async Task<Project?> GetByIdAsync(int id) =>
            (await _db.LoadDataAsync<Project, dynamic>("dbo.spProject_Get", new { Id = id })).FirstOrDefault();

        public async Task UpdateAsync(Project project) =>
            await _db.SaveDataAsync("dbo.spProject_Edit", new { project.Id, project.Title, project.Description });

        public async Task DeleteAsync(int id) =>
            await _db.SaveDataAsync("dbo.spProject_Delete", new { Id = id });
    }
}
