using System.Data.SqlClient;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly ISQLDataAccess _db;
    public ProjectRepository(ISQLDataAccess db)
    {
        _db = db;
    }

    public async Task<int> AddAsync(Project project)
    {
        var result = (await _db.SaveDataAndGetIdAsync<dynamic, int>("dbo.spProject_Insert", new { project.Title, project.Description, Created = DateTime.UtcNow }));
        return result;

    }
    public async Task<IEnumerable<Project>> GetAllAsync() =>
        await _db.LoadDataAsync<Project>("dbo.spProject_GetAll");

    public async Task<Project?> GetByIdAsync(int id) =>
        (await _db.LoadDataAsync<Project, dynamic>("dbo.spProject_Get", new { Id = id })).FirstOrDefault();

    public async Task UpdateAsync(Project project)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spProject_Edit", new { project.Id, project.Title, project.Description });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _db.SaveDataAsync("dbo.spProject_Delete", new { Id = id });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
}

