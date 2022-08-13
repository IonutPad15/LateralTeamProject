using DataAccess.Data.IData;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public class IssueData : IIssueData
    {
        private readonly ISQLDataAccess _db;
        public IssueData(ISQLDataAccess db)
        {
            _db = db;
        }
        public async Task AddAsync(Issue entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            await _db.SaveDataAsync("dbo.spIssue_Insert", new {
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
        }
        public async Task<IEnumerable<Issue>> GetAllAsync() =>
            await _db.LoadDataAsync<Issue, Project, Status, Priority, Role, IssueType, User>("dbo.spIssue_GetAll");
        public async Task<Issue?> GetByIdAsync(int id) =>
            (await _db.LoadDataAsync<Issue, Project, Status, Priority, Role, IssueType, User, dynamic>("dbo.spIssue_Get", new { Id = id })).FirstOrDefault();
        public async Task UpdateAsync(Issue entity)
        {
            entity.Updated = DateTime.UtcNow;
            await _db.SaveDataAsync(
                "dbo.spIssue_Update", 
                new {
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
        public async Task DeleteAsync(int id) =>
            await _db.SaveDataAsync("dbo.spIssue_Delete", new {Id = id});
        public async Task NextPreview(int id, string method = "next")
        {
            var issue = (await _db.LoadDataAsync<Issue, dynamic>("dbo.spIssue_Get", new { Id = id })).FirstOrDefault();
            if(issue == null) return;
            await _db.SaveDataAsync("dbo.spIssue_NextPreviewStatus", new { Id = id, Method = method, StatusId = issue.StatusId });
        }
    }
}
