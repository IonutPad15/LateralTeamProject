using System.Data.SqlClient;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Repository;

public class IssueRepository : IIssueRepository
{
    private readonly ISQLDataAccess _db;
    public IssueRepository(ISQLDataAccess db)
    {
        _db = db;
    }
    public async Task<int> AddAsync(Issue entity)
    {
        try
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            var result = await _db.SaveDataAndGetIdAsync<dynamic, int>("dbo.spIssue_Insert", new
            {
                Title = entity.Title,
                IssueTypeId = entity.IssueTypeId,
                Created = entity.Created,
                Updated = entity.Updated,
                RoleId = entity.RoleId,
                StatusId = entity.StatusId,
                ProjectId = entity.ProjectId,
                Description = entity.Description,
                UserAssignedId = entity.UserAssignedId,
                PriorityId = entity.PriorityId
            });
            return result;
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
    public async Task<IEnumerable<Issue>> GetAllAsync()
    {
        return await _db.LoadDataAsync<Issue, Project, Status, Priority, Role, IssueType, User>("dbo.spIssue_GetAll");
    }
    public async Task<Issue?> GetByIdAsync(int id)
    {
        return (await _db.LoadDataAsync<Issue, Project, Status, Priority, Role, IssueType, User, dynamic>("dbo.spIssue_Get", new { Id = id })).FirstOrDefault();
    }
    public async Task UpdateAsync(Issue entity)
    {
        try
        {
            entity.Updated = DateTime.UtcNow;
            await _db.SaveDataAsync(
                "dbo.spIssue_Update",
                new
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    Updated = entity.Updated,
                    ProjectId = entity.ProjectId,
                    RoleId = entity.RoleId,
                    IssueTypeId = entity.IssueTypeId,
                    UserAssignedId = entity.UserAssignedId,
                    PriorityId = entity.PriorityId,
                    StatusId = entity.StatusId
                });
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
            await _db.SaveDataAsync("dbo.spIssue_Delete", new { Id = id });
        }
        catch (SqlException ex)
        {
            throw new RepositoryException(ex.Message);
        }
    }
    public async Task NextStatusOfIssueAsync(int id, int statusId)
    {
        await _db.SaveDataAsync("dbo.spIssue_UpdateStatus", new { Id = id, StatusId = statusId });
    }
    public async Task PreviousStatusOfIssueAsync(int id, int statusId)
    {
        await _db.SaveDataAsync("dbo.spIssue_UpdateStatus", new { Id = id, StatusId = statusId });
    }
    public async Task<int> GetProjectId(int id)
    {
        return (await _db.LoadDataAsync<int, dynamic>("spIssue_GetProjectId", new { Id = id })).FirstOrDefault();
    }
}
