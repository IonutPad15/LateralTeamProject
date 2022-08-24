// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using DataAccess.Data.IData;
using System.Data;
using System.Data.SqlClient;


namespace IssueTracker.UnitTest;

public abstract class BaseClass
{
    public static IIssueData IssueData = null!;
    public static TestContext TestContext { get; set; } = null!;
    public static DataTable? TestDataTable { get; set; }
    public static IParticipantData ParticipantData = null!;
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
