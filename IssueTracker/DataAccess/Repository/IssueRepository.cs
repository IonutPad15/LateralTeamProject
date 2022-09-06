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
    public async Task<IEnumerable<Issue>> GetAllAsync()
    {
        return await _db.LoadIssueAsync<dynamic>("dbo.spIssue_GetAll", new { });
    }
    public async Task<Issue?> GetByIdAsync(int id)
    {
        return (await _db.LoadIssueAsync<dynamic>("dbo.spIssue_Get", new { Id = id })).FirstOrDefault();
    }
    public async Task UpdateAsync(Issue entity)
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
    public async Task DeleteAsync(int id)
    {
        await _db.SaveDataAsync("dbo.spIssue_Delete", new { Id = id });
    }
    public async Task NextStatusOfIssueAsync(int id, int statusId)
    {
        await _db.SaveDataAsync("dbo.spIssue_UpdateStatus", new { Id = id, StatusId = statusId });
    }
    public async Task PreviousStatusOfIssueAsync(int id, int statusId)
    {
        await _db.SaveDataAsync("dbo.spIssue_UpdateStatus", new { Id = id, StatusId = statusId });
    }
}
