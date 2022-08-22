using DataAccess.Data;
using DataAccess.Data.IData;
using DataAccess.DbAccess;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace IssueTracker.UnitTest
{
    public abstract class BaseClass
    {
        protected readonly IIssueData issueData;
        protected TestContext testContext { get; set; }
        public BaseClass()
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
               .AddJsonFile("appsettings.json", optional: false)
               .Build();
            issueData = new IssueData(new SQLDataAccess(configuration));  //moq
        }
    }
}
