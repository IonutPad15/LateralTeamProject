using DataAccess.Models;
using System.Data;
using System.Data.SqlClient;

namespace IssueTracker.UnitTest;

[TestClass]
public class ParticipantDataTest : BaseClass
{
    [TestMethod()]
    [Description("Given a valid request, when GetAllAsync is called" +
        "then it has at least one participant")]
    public async Task GetAll_Test()
    {
        var participants = await ParticipantData.GetAllAsync();
        Assert.IsTrue(participants.Count() > 0);
    }
    [TestMethod]
    [Description("Given a valid participant object, when AddAsync is called" +
        "then it should add a participant in the DB")]
    public async Task AddAsync_Test()
    {
        //arrange
        var userID = Constants.User.Id;
        Participant participant = new Participant()
        {
            ProjectId = 2,
            RoleId = (RolesType)4,
            UserId = userID
        };
        var participantId = await ParticipantData.AddAsync(participant);
        //act
        var result = await ParticipantData.GetByIdAsync(participantId);
        //assert
        Assert.AreEqual(participantId, result!.Id);
    }
    [TestMethod]
    [Description("Given an invalid request (UserId = new Guid() and ProjectId = 0), when GetAllAsync is called" +
        "then it should throw an SQLException (it violates the FK rule)")]
    public async Task AddAsyncFkException_Test()
    {
        Participant participant = new Participant()
        {
            ProjectId = 0,
            RoleId = (RolesType)1,
            UserId = new Guid()
        };
        await Assert.ThrowsExceptionAsync<SqlException>(() => ParticipantData.AddAsync(participant));
    }
    [TestMethod]
    [Description("Given an invalid request (id <= 0), when DeleteAsync is called" +
        "then it should throw an SQLException")]
    public async Task DeleteAsync_Test_BadId()
    {
        await Assert.ThrowsExceptionAsync<SqlException>(() => ParticipantData.DeleteAsync(-1));
    }
    [TestMethod]
    [Description("Given a valid request (id = 2), when DeleteAsync is called" +
        "then delets the participant")]
    public async Task DeleteAsync_Test()
    {
        await ParticipantData.DeleteAsync(2);
    }
    [TestMethod]
    [Description("Given a valid request (id = 1), when GetByIdAsync is called, then returns that participant! ")]
    public async Task GetByIdAsync_Test()
    {
        var participant = await ParticipantData.GetByIdAsync(1);
        Assert.IsNotNull(participant);
    }
    [TestMethod]
    [Description("Given an invalid request (id = 0), when GetByIdAsync is called" +
        "then it return a null object")]
    public async Task GetByIdAsync_Test_BadId()
    {
        var participant = await ParticipantData.GetByIdAsync(0);
        Assert.IsNull(participant);
    }
    [TestMethod]
    [Description("Given a valid request (projectId = 1), when GetOwnersAndCollabsByProjectIdAsync is called" +
        "then returns a list of participants with these roles")]
    public async Task GetOwnersAndCollabsByProjectIdAsync_Test()
    {
        var participants = await ParticipantData.GetOwnersAndCollabsByProjectIdAsync(1);
        TestContext.WriteLine(participants.Count().ToString());
        Assert.IsTrue(participants.Any());
    }
    [TestMethod]
    [Description("Given an invalid request (projectId = 0), when GetOwnersAndCollabsByProjectIdAsync is called" +
        "then it should be an empty list")]
    public async Task GetOwnersAndCollabsByProjectIdAsync_Test_BadRequest()
    {
        var participants = await ParticipantData.GetOwnersAndCollabsByProjectIdAsync(0);
        Assert.IsTrue(!participants.Any());
    }
    [TestMethod]
    [Description("Given a valid request (projectId = 1), when GetOwnerByProjectIdAsync is called" +
        "then it should be a single Participant object")]
    public async Task GetOwnerByProjectIdAsync_Test()
    {
        var participants = await ParticipantData.GetOwnerByProjectIdAsync(1);
        Assert.IsTrue(participants.Count() == 1);
    }
    [TestMethod]
    [Description("Given an invalid request (projectId = 0), when GetOwnerByProjectIdAsync is called" +
        "then it should be an empty list or too many elements ")]
    public async Task GetOwnerByProjectIdAsync_Test_BadRequest()
    {
        var participants = await ParticipantData.GetOwnerByProjectIdAsync(0);
        Assert.IsTrue(participants.Count() != 1);
    }
    [TestMethod]
    [Description("Given a valid request, when UpdateAsync is called" +
        "then it update the role of that participant")]
    public async Task UpdateAsync_Test()
    {
        string? conn = TestContext.Properties["ConnectionString"]!.ToString();
        string sql = "SELECT * FROM dbo.[Participant] WHERE IsDeleted = 0";
        LoadDataTable(sql, conn!);
        if (TestDataTable!.Rows.Count > 0)
        {
            var row = TestDataTable!.Rows[0];
            Participant participant = new Participant()
            {
                Id = row.Field<int>("Id"),
                UserId = row.Field<Guid>("UserId"),
                ProjectId = row.Field<int>("ProjectId"),
                RoleId = (RolesType)1
            };
            await ParticipantData.UpdateAsync(participant);
        }
        else
        {
            Assert.Fail();
        }
    }
    [TestMethod]
    [Description("Given an invalid RoleId (-1), when UpdateAsync is called" +
        "then it should throw an SqlException")]
    public async Task UpdateAsync_Test_BadRoleId()
    {
        string? conn = TestContext.Properties["ConnectionString"]!.ToString();
        string sql = "SELECT * FROM dbo.[Participant] WHERE IsDeleted = 0";
        LoadDataTable(sql, conn!);
        var row = TestDataTable!.Rows[0];
        Participant participant = new Participant()
        {
            Id = row.Field<int>("Id"),
            UserId = row.Field<Guid>("UserId"),
            ProjectId = row.Field<int>("ProjectId"),
            RoleId = (RolesType)(-1)
        };
        await Assert.ThrowsExceptionAsync<SqlException>(() => ParticipantData.UpdateAsync(participant));
    }
    [TestMethod]
    [Description("Given an invalid Id (-1), when UpdateAsync is called" +
        "then it should throw an SqlException")]
    public async Task UpdateAsync_Test_BadId()
    {
        string? conn = TestContext.Properties["ConnectionString"]!.ToString();
        string sql = "SELECT * FROM dbo.[Participant] WHERE IsDeleted = 0";
        LoadDataTable(sql, conn!);
        var row = TestDataTable!.Rows[0];
        Participant participant = new Participant()
        {
            Id = -1,
            UserId = row.Field<Guid>("UserId"),
            ProjectId = row.Field<int>("ProjectId"),
            RoleId = (RolesType)4
        };
        await Assert.ThrowsExceptionAsync<SqlException>(() => ParticipantData.UpdateAsync(participant));
    }
}
