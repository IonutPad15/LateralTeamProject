using System.Data;
using System.Data.SqlClient;
using DataAccess.Data.IData;

namespace IssueTracker.UnitTest;

public abstract class BaseClass
{
    public static IIssueData issueData;
    public static TestContext testContext;
    public static DataTable? TestDataTable { get; set; }
    public static IParticipantData _participantData;

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
        catch
        {
            testContext.WriteLine("Error in LoadDataTable() method.");
        }
        return TestDataTable;
    }
}
