using DataAccess;
using DataAccess.Models;

namespace IssueTracker.UnitTest;

[TestClass]
public class IssueDataTest : BaseClass
{
    [TestMethod]
    public async Task GetAllIssue_ReturnListIssue()
    {
        var issueList = await IssueRepository.GetAllAsync();

        Assert.IsTrue(issueList.Count() > 0);
    }

    [TestMethod]
    [Description("Given request in db WHEN id = 0 THEN return null!")]
    public async Task GetIssueById0()
    {
        var result = await IssueRepository.GetByIdAsync(0);
        Assert.IsNull(result);
    }

    [TestMethod]
    [Description("Given request in db WHEN id = 1 THEN return success!")]
    public async Task GetIssueById1()
    {
        var issue = await IssueRepository.GetByIdAsync(1);

        Assert.IsNotNull(issue);
    }

    [TestMethod]
    [Description("Given with corect data in issue object WHEN updateAsync is call THEN return success")]
    public async Task Updated()
    {
        var issue = new Issue
        {
            Id = 1,
            StatusId = 3,
            Description = "test",
            PriorityId = 1,
            UserAssignedId = Guid.Parse("13E12278-2EB1-4FC7-9C20-639A9CFC8F21"),
            Updated = DateTime.UtcNow,
            Title = "test",
            ProjectId = 2,
            RoleId = (RolesType)3,
            IssueTypeId = 2
        };
        await IssueRepository.UpdateAsync(issue);
        Assert.IsTrue(true);
    }

    [TestMethod]
    [Description("Given without title issue object WHEN updateAsync is call THEN return error")]
    public async Task Updated_EntityWithoutTitle_Error()
    {
        var issue = new Issue
        {
            Id = 1,
            StatusId = 3,
            Description = "test",
            PriorityId = 1,
            UserAssignedId = Guid.NewGuid(),
            Updated = DateTime.UtcNow,
            ProjectId = 2,
            RoleId = (RolesType)3,
            IssueTypeId = 2
        };
        await Assert.ThrowsExceptionAsync<RepositoryException>(() => IssueRepository.UpdateAsync(issue));
    }
}
