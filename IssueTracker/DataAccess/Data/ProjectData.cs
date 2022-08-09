using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;
using Models.Request;

namespace DataAccess.Data
{
    public class ProjectData : IProjectData
    {
        private readonly ISQLDataAccess _db;
        public ProjectData(ISQLDataAccess db)
        {
            _db= db;
        }

        public async Task AddAsync(ProjectRequest project) =>
            await _db.SaveData("dbo.spProject_Insert", new { project.Title, project.Description, DateTime.Now });

        public async Task<IEnumerable<Project>> GetAllAsync() =>
            await _db.LoadData<Project>("dbo.spProject_GetAll");

        public async Task<Project?> GetByIdAsync(int id) =>
            (await _db.LoadData<Project, dynamic>("dbo.spProject_Get", new { Id = id })).FirstOrDefault();

        public async Task UpdateAsync(ProjectRequest project) =>
            await _db.SaveData("dbo.spProject_Edit", new { project.Id, project.Title, project.Description });

        public async Task DeleteAsync(int id) =>
            await _db.SaveData("dbo.spProject_Delete", new { Id = id });
    }
}
