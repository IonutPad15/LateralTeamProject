using DataAccess.Repository;
using DataAccess.DbAccess;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using IssueTracker.FileSystem;
using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest;

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
            BaseClass.IssueData = new IssueRepository(new SQLDataAccess(configuration));
            BaseClass.ParticipantData = new ParticipantRepository(new SQLDataAccess(configuration));
            BaseClass.TestContext = tc;
            BaseClass.FileObject = new File();

            IConfigurationFactory cf = new ConfigurationFactory(configuration);
            var blobConfig = cf.Create<IBolbConfigurationFactory>();
            var metadataconfig = cf.Create<IMetaDataConfiguration>();
            BaseClass.s_blobData = new BolbStorageProvider(blobConfig);
            BaseClass.s_metaDataProvider = new MetaData(metadataconfig);
            BaseClass.s_fileProviderData = new FileProvider(configuration);

            //begin populate Blob Test
            var path = "TextForTest.txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            var idFileTest = Guid.NewGuid().ToString();
            using (FileStream fs = System.IO.File.Create(path))
            {
                BaseClass.FileObject.Id = idFileTest;
                BaseClass.FileObject.Name = "TextForTest";
                BaseClass.FileObject.Content = fs;
                BaseClass.FileObject.BlobName = $"{idFileTest}.txt";
                BaseClass.FileObject.Extension = ".txt";
                BaseClass.FileObject.SizeKb = 12;
                BaseClass.FileObject.Type = "TextDocument";
            };
            var file = new File
            {
                BlobName = $"{BaseClass.FileObject.Id}.txt",
                Content = BaseClass.FileObject.Content
            };
            BaseClass.s_blobData.UploadFileAsync(file);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            //End populate Blob Test

            string? conn = configuration["ConnectionStrings.Default"].ToString();
            string sql = "TRUNCATE TABLE dbo.[Participant]";
            using (SqlConnection ConnectionObject = new SqlConnection(conn!))
            {

                ConnectionObject.Open();
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    int rows = CommandObject.ExecuteNonQuery();
                }
                sql = "DELETE FROM dbo.[Issue]";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    int rows = CommandObject.ExecuteNonQuery();
                }
                sql = "DELETE FROM dbo.[User]";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    int rows = CommandObject.ExecuteNonQuery();
                }

                sql = "DELETE FROM dbo.[Project]";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    int rows = CommandObject.ExecuteNonQuery();
                }
                sql = "INSERT INTO dbo.[User] VALUES(@Id,@UserName,@Email,@Password,@IsDeleted)";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    CommandObject.Parameters.AddWithValue("@Id", Constants.User.Id);
                    CommandObject.Parameters.AddWithValue("@UserName", Constants.User.UserName);
                    CommandObject.Parameters.AddWithValue("@Email", Constants.User.Email);
                    CommandObject.Parameters.AddWithValue("@Password", Constants.User.Password);
                    CommandObject.Parameters.AddWithValue("@IsDeleted", Constants.User.IsDeleted);
                    CommandObject.ExecuteNonQuery();

                }
                sql = "SET IDENTITY_INSERT dbo.[Project] ON INSERT INTO dbo.[Project](Id,Title, Description, Created) VALUES (@Id, @Title, @Description, @Created)";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    CommandObject.Parameters.AddWithValue("@Id", 1);
                    CommandObject.Parameters.AddWithValue("@Title", "project");
                    CommandObject.Parameters.AddWithValue("@Description", "description");
                    CommandObject.Parameters.AddWithValue("@Created", DateTime.Now);
                    CommandObject.ExecuteNonQuery();
                }
                sql = "SET IDENTITY_INSERT dbo.[Project] ON INSERT INTO dbo.[Project](Id,Title, Description, Created) VALUES (@Id, @Title, @Description, @Created)";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    CommandObject.Parameters.AddWithValue("@Id", 2);
                    CommandObject.Parameters.AddWithValue("@Title", "project2");
                    CommandObject.Parameters.AddWithValue("@Description", "description2");
                    CommandObject.Parameters.AddWithValue("@Created", DateTime.Now);
                    CommandObject.ExecuteNonQuery();
                }
                sql = "SET IDENTITY_INSERT dbo.[Issue] ON INSERT INTO dbo.[Issue] (Id, Title, Description, "
                    + "Created, Updated ,IssueTypeId, ProjectId, UserAssignedId , PriorityId, StatusId, RoleId, IsDeleted)"
                    + "VALUES (@Id, @Title, @Description, @Created, @Updated ,@IssueTypeId, @ProjectId, @UserAssignedId , @PriorityId, @StatusId, @RoleId, @IsDeleted)";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    CommandObject.Parameters.AddWithValue("@Id", 1);
                    CommandObject.Parameters.AddWithValue("@Title", "issue1");
                    CommandObject.Parameters.AddWithValue("@Description", "description1");
                    CommandObject.Parameters.AddWithValue("@Created", DateTime.Now);
                    CommandObject.Parameters.AddWithValue("@Updated", DateTime.Now);
                    CommandObject.Parameters.AddWithValue("@IssueTypeId", 1);
                    CommandObject.Parameters.AddWithValue("@ProjectId", 1);
                    CommandObject.Parameters.AddWithValue("UserAssignedId", Constants.User.Id);
                    CommandObject.Parameters.AddWithValue("@PriorityId", 1);
                    CommandObject.Parameters.AddWithValue("@StatusId", 1);
                    CommandObject.Parameters.AddWithValue("@RoleId", 1);
                    CommandObject.Parameters.AddWithValue("@IsDeleted", false);
                    CommandObject.ExecuteNonQuery();

                }
                var userID = Constants.User.Id;
                Participant participant = new Participant();
                if (userID != Guid.Empty)
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
                    participant.ProjectId = 2;
                    CommandObject.Parameters.Clear();
                    CommandObject.Parameters.AddWithValue("@UserId", participant.UserId);
                    CommandObject.Parameters.AddWithValue("@ProjectId", participant.ProjectId);
                    CommandObject.Parameters.AddWithValue("@RoleId", participant.RoleId);
                    CommandObject.ExecuteNonQuery();
                }
                ConnectionObject.Close();
            }
        }
        catch (Exception ex)
        {
            tc.WriteLine(ex.Message);
        }

    }

    [AssemblyCleanup()]
    public static void AssemblyCleanup()
    {

        TestContext tc = BaseClass.TestContext;
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
                sql = "DELETE FROM dbo.[Issue]";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    int rows = CommandObject.ExecuteNonQuery();
                }
                sql = "DELETE FROM dbo.[User]";
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    int rows = CommandObject.ExecuteNonQuery();
                }
                sql = "DELETE FROM dbo.[Project]";
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
