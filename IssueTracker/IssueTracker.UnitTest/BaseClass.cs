using DataAccess.Repository;
using IssueTracker.FileSystem;
using File = IssueTracker.FileSystem.Models.File;
using System.Data;
using System.Data.SqlClient;

namespace IssueTracker.UnitTest;
public abstract class BaseClass
{
    internal static IIssueRepository IssueRepository { get; set; } = null!;
    internal static TestContext TestContext { get; set; } = null!;
    internal static File File { get; set; } = null!;
    internal static DataTable? TestDataTable { get; set; }
    internal static IParticipantRepository ParticipantRepository { get; set; } = null!;
    internal static IFileProvider TestFileProvider { get; set; } = null!;
    internal static IMetaDataProvider TestMetaDataProvider { get; set; } = null!;
    internal static IBlobProvider TestBlobProvider { get; set; } = null!;

    public static DataTable? LoadDataTable(string sql, string connection)
    {
        TestDataTable = null;
        try
        {
            using (SqlConnection ConnectionObject = new SqlConnection(connection))
            {
                ConnectionObject.Open();
                using (SqlCommand CommandObject = new SqlCommand(sql, ConnectionObject))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(CommandObject))
                    {
                        TestDataTable = new DataTable();
                        da.Fill(TestDataTable);
                    }
                }
                ConnectionObject.Close();
            }
        }
        catch (Exception)
        {
            TestContext.WriteLine("Error in LoadDataTable() method.");
        }
        return TestDataTable;
    }
}
