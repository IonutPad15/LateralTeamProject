
using DataAccess.Data;
using DataAccess.DbAccess;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace IssueTracker.UnitTest
{
    [TestClass]
    public class TestInitialization
    {

        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext tc)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
               .AddJsonFile("appsettings.json", optional: false)
               .Build();
                BaseClass.issueData = new IssueData(new SQLDataAccess(configuration)); 
                BaseClass._participantData = new ParticipantData(new SQLDataAccess(configuration));
                BaseClass.testContext = tc;


                string? conn = tc.Properties["ConnectionString"]!.ToString();
                string sql = "TRUNCATE TABLE dbo.[Participant]";
                using (SqlConnection ConnectionObject = new SqlConnection(conn!))
                {
                    ConnectionObject.Open();
                    using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                    {
                        int rows = CommandObject.ExecuteNonQuery();
                    }
                    sql = "SELECT * FROM dbo.[User]";
                    var userID = Guid.Empty;
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
                    Participant participant = new Participant();
                    if (userID!= Guid.Empty)
                    {
                        participant = new Participant()
                        {
                            UserId = userID,
                            ProjectId = 1,
                            RoleId = (RolesType)3
                        };
                    }
                    sql = $"INSERT INTO dbo.[Participant] (UserId, ProjectId, RoleId) Values(@UserId, @ProjectId, @RoleId)";
                    using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                    {
                        CommandObject.Parameters.AddWithValue("@UserId", participant.UserId);
                        CommandObject.Parameters.AddWithValue("@ProjectId", participant.ProjectId);
                        CommandObject.Parameters.AddWithValue("@RoleId", participant.RoleId);
                        CommandObject.ExecuteNonQuery();
                        participant.RoleId = (RolesType)1;
                        CommandObject.Parameters.Clear();
                        CommandObject.Parameters.AddWithValue("@UserId", participant.UserId);
                        CommandObject.Parameters.AddWithValue("@ProjectId", participant.ProjectId);
                        CommandObject.Parameters.AddWithValue("@RoleId", participant.RoleId);
                        CommandObject.ExecuteNonQuery();
                        participant.RoleId = (RolesType)2;
                        CommandObject.Parameters.Clear();
                        CommandObject.Parameters.AddWithValue("@UserId", participant.UserId);
                        CommandObject.Parameters.AddWithValue("@ProjectId", participant.ProjectId);
                        CommandObject.Parameters.AddWithValue("@RoleId", participant.RoleId);
                        CommandObject.ExecuteNonQuery();
                    }
                    ConnectionObject.Close();
                }
            }
            catch(Exception ex)
            {
                tc.WriteLine(ex.Message);
            }

        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            TestContext tc = BaseClass.testContext;
            try
            {
                
                string? conn = tc.Properties["ConnectionString"]!.ToString();
                string sql = "TRUNCATE TABLE dbo.[Participant]";
                using (SqlConnection ConnectionObject = new SqlConnection(conn!))
                {
                    ConnectionObject.Open();
                    using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                    {
                        int rows = CommandObject.ExecuteNonQuery();
                        
                    }
                    ConnectionObject.Close();
                }
            }
            catch (Exception ex)
            {
                tc.WriteLine(ex.Message);
            }
        }
    }
}
