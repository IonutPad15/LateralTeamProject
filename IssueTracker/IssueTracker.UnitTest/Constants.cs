
using DataAccess.Models;
using DataAccess.Utils;

namespace IssueTracker.UnitTest;
public class Constants
{
    public static readonly User User;
    static Constants()
    {
        HashHelper hp = new HashHelper();
        User = new User()
        {
            UserName = "tester",
            Email = "tester@gmail.com",
            Password = hp.GetHash("parolatester"),
            Id = Guid.Parse("13E12278-2EB1-4FC7-9C20-639A9CFC8F21"),
            IsDeleted = false
        };
    }
}
