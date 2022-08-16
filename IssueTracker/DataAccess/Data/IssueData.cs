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
        public async Task<IEnumerable<Issue>> GetAllAsync()
        {
            return await _db.LoadDataAsync<Issue, Project, Status, Priority, Role, IssueType, User>("dbo.spIssue_GetAll");
        }
        public async Task<Issue?> GetByIdAsync(int id)
        {
            var listOfTypes = new List<Type>();
            listOfTypes.Add(typeof(Project));
            listOfTypes.Add(typeof(Status));
            listOfTypes.Add(typeof(Priority));
            listOfTypes.Add(typeof(Role));
            listOfTypes.Add(typeof(IssueType));
            listOfTypes.Add(typeof(User));
            return (await _db.LoadDataAsync<Issue, Project, Status, Priority, Role, IssueType, User, dynamic>("dbo.spIssue_Get", new { Id = id })).FirstOrDefault();
        }
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
        public async Task DeleteAsync(int id)
        {
            await _db.SaveDataAsync("dbo.spIssue_Delete", new {Id = id});
        }
        public async Task NextStatusOfIssueAsync(int id, int statusId)
        {
            await _db.SaveDataAsync("dbo.spIssue_UpdateStatus", new { Id = id, StatusId = statusId });
        }
        public async Task PreviewStatusOfIssueAsync(int id, int statusId)
        {
            await _db.SaveDataAsync("dbo.spIssue_UpdateStatus", new { Id = id, StatusId = statusId });
        }
    }
}
