using DataAccess.Repository;
using IssueTracker.FileSystem;
using System.Data;
using System.Data.SqlClient;

namespace IssueTracker.UnitTest;
public abstract class BaseClass
{
    public static IIssueRepository IssueData = null!;
    public static TestContext TestContext { get; set; } = null!;
    public static DataTable? TestDataTable { get; set; }
    public static IParticipantRepository ParticipantData = null!;
    public static IFileProvider FileProviderData = null!;
    internal static IMetaDataProvider s_metaDataProvider = null!;

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
