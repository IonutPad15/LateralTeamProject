using DataAccess.Models;
using Microsoft.Extensions.Configuration;

namespace IssueTracker.UnitTest
{
    [TestClass]
    public class IssueDataTest : BaseClass
    {

        public IssueDataTest() 
        {
        }
        

        [TestMethod]
        public async Task GetAllIssue_AreSame_ReturnListIssue()
        {
            var issueList = await issueData.GetAllAsync();

            Assert.IsTrue(issueList.Count() > 0);
        }

        [TestMethod]
        [Owner("P.Claudiu")]
        [Description("Return null, db don't have this id!")]
        public async Task GetIssueById0()
        {
            var issue = await issueData.GetByIdAsync(0);

            Assert.IsNull(issue);
        }
        
        [TestMethod]
        [Owner("P.Claudiu")]
        [Description("Return entity, db have id 1!")]
        public async Task GetIssueById1()
        {
            var issue = await issueData.GetByIdAsync(1);

            Assert.IsNotNull(issue);
        }

        [TestMethod]
        [Owner("P.Claudiu")]
        [Description("Work, entity is correct!")]
        public async Task Updated()
        {
            var issue = new Issue{
                Id = 1,
                StatusId = 3,
                Description = "test",
                PriorityId = 1,
                UserAssignedId = Guid.NewGuid(),
                Updated = DateTime.UtcNow,
                Title = "test",
                ProjectId = 1,
                RoleId = 3,
                IssueTypeId = 2
            };
                await issueData.UpdateAsync(issue);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [Owner("P.Claudiu")]
        [Description("Don't Work, entity isn't correct!")]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Updated_EntityNotCorrect_Error()
        {
            var issue = new Issue
            {
                Id = 1,
                StatusId = 3,
                Description = "test",
                PriorityId = 1,
                UserAssignedId = Guid.NewGuid(),
                Updated = DateTime.UtcNow,
                ProjectId = 1,
                RoleId = 3,
                IssueTypeId = 2
            };
            await issueData.UpdateAsync(issue);
            Assert.IsTrue(true);
        }
    }
}
