using DataAccess.Data;
using DataAccess.Data.IData;
using DataAccess.DbAccess;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace IssueTracker.UnitTest
{
    public abstract class BaseClass
    {
        public static IIssueData issueData;
        public static TestContext testContext { get; set; }
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
            catch (Exception)
            {
                testContext.WriteLine("Error in LoadDataTable() method.");
            }
            return TestDataTable;
        }
    }
}
