using DataAccess.Models;
using System.Data;
using System.Data.SqlClient;

namespace IssueTracker.UnitTest;

[TestClass]
public class ParticipantDataTest : BaseClass
{
    [TestMethod()]
    [Description("Given a valid request, when GetAllAsync is called" +
        "then it should be a success")]
    public async Task GetAll_Test()
    {
        string sql = "SELECT * FROM dbo.[Participant]";

        string? conn = TestContext.Properties["ConnectionString"]!.ToString();
        TestContext.WriteLine(conn);
        LoadDataTable(sql, conn!);
        var participants = await ParticipantData.GetAllAsync();
        int value = participants.Count();
        int expected = TestDataTable!.Rows.Count;
        TestContext.WriteLine("Testing _participant.GetAllAsync, Expected Value: '{0}', " +
            "Actual Value: '{1}', Result: '{2}'",
            value, expected, (value == expected ? "Succes" : "Failed"));
        if (value != expected)
        {
            Assert.Fail("Data Driven Tests Have Failed, Check Additional Output For More Info");
        }
    }
    [TestMethod]
    [Description("Given a valid request, when AddAsync is called" +
        "then it should be a success")]
    public async Task AddAsync_Test()
    {
        string? conn = TestContext.Properties["ConnectionString"]!.ToString();
        string sql = "SELECT * FROM dbo.[User]";
        var userID = Guid.Empty;
        using (SqlConnection ConnectionObject = new SqlConnection(conn!))
        {
            ConnectionObject.Open();
            using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(CommandObject))
                {
                    DataTable TestDataTable = new DataTable();
                    da.Fill(TestDataTable);
                    var row = TestDataTable.Rows[0];
                    userID = Guid.Parse(row["Id"].ToString()!);

                }
            }
            ConnectionObject.Close();
        }
        Participant participant = new Participant()
        {
            ProjectId = 1,
            RoleId = (RolesType)4,
            UserId = userID
        };
        await ParticipantData.AddAsync(participant);
    }
    [TestMethod]
    [ExpectedException(typeof(SqlException))]
    [Description("Given an invalid request, when GetAllAsync is called" +
        "then it should throw an SQLException")]
    public async Task AddAsyncFkException_Test()
    {
        Participant participant = new Participant()
        {
            ProjectId = 0,
            RoleId = (RolesType)1,
            UserId = new Guid()
        };
        await ParticipantData.AddAsync(participant);

    }
    [TestMethod]
    [ExpectedException(typeof(SqlException))]
    [Description("Given an invalid request, when DeleteAsync is called" +
        "then it should throw an SQLException")]
    public async Task DeleteAsync_Test_BadId()
    {
        await ParticipantData.DeleteAsync(-1);
    }
    [TestMethod]
    [Description("Given a valid request, when DeleteAsync is called" +
        "then it should be a success")]
    public async Task DeleteAsync_Test()
    {
        await ParticipantData.DeleteAsync(2);
    }
    [TestMethod]
    [Description("Given a valid request, when GetByIdAsync is called" +
        "then a participant is returned")]
    public async Task GetByIdAsync_Test()
    {
        var participant = await ParticipantData.GetByIdAsync(1);
        Assert.IsNotNull(participant);
    }
    [TestMethod]
    [Description("Given an invalid request, when GetByIdAsync is called" +
        "then it should throw an SQLException")]
    public async Task GetByIdAsync_Test_BadId()
    {
        var participant = await ParticipantData.GetByIdAsync(0);
        Assert.IsNull(participant);
    }
    [TestMethod]
    [Description("Given a valid request, when GetOwnersAndCollabsByProjectIdAsync is called" +
        "then it should be a success")]
    public async Task GetOwnersAndCollabsByProjectIdAsync_Test()
    {
        var participants = await ParticipantData.GetOwnersAndCollabsByProjectIdAsync(1);
        TestContext.WriteLine(participants.Count().ToString());
        Assert.IsTrue(participants.Any());
    }
    [TestMethod]
    [Description("Given an invalid request, when GetOwnersAndCollabsByProjectIdAsync is called" +
        "then it should be an empty list")]
    public async Task GetOwnersAndCollabsByProjectIdAsync_Test_BadRequest()
    {
        var participants = await ParticipantData.GetOwnersAndCollabsByProjectIdAsync(0);
        Assert.IsTrue(!participants.Any());
    }
    [TestMethod]
    [Description("Given a valid request, when GetOwnerByProjectIdAsync is called" +
        "then it should be a single Participant object")]
    public async Task GetOwnerByProjectIdAsync_Test()
    {
        var participants = await ParticipantData.GetOwnerByProjectIdAsync(1);
        Assert.IsTrue(participants.Count() == 1);
    }
    [TestMethod]
    [Description("Given an invalid request, when GetOwnerByProjectIdAsync is called" +
        "then it should be an empty list or too many elements ")]
    public async Task GetOwnerByProjectIdAsync_Test_BadRequest()
    {
        var participants = await ParticipantData.GetOwnerByProjectIdAsync(0);
        Assert.IsTrue(participants.Count() != 1);
    }
    [TestMethod]
    [Description("Given a valid request, when UpdateAsync is called" +
        "then it should be a success")]
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
                RoleId = (RolesType)2
            };
            await ParticipantData.UpdateAsync(participant);
        }
        else
        {
            Assert.Fail();
        }
    }
    [TestMethod]
    [ExpectedException(typeof(SqlException))]
    [Description("Given an invalid Id, when UpdateAsync is called" +
        "then it should throw an SqlException")]
    public async Task UpdateAsync_Test_BadId()
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
        await ParticipantData.UpdateAsync(participant);
    }
    [TestMethod]
    [ExpectedException(typeof(SqlException))]
    [Description("Given an invalid RoleId, when UpdateAsync is called" +
        "then it should throw an SqlException")]
    public async Task UpdateAsync_Test_BadRoleId()
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
        await ParticipantData.UpdateAsync(participant);
    }
}
