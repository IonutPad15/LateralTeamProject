using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest.FileSystem;
[TestClass]
public class FileProviderTest : BaseClass
{
    [TestMethod]
    [Description("Test Get method with id wrong and extension good, i expected exception")]
    [ExpectedException(typeof(ArgumentException))]
    public void GetFiles_IdWrong()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File(Guid.NewGuid().ToString(), ".png");
        filesModels.Add(fileModel);
        IEnumerable<File> files = filesModels;
        TestFileProvider.GetAsync(files);
    }

    [TestMethod]
    [Description("Test Get method with id good and extension good, i expected result")]
    public void GetFiles_IdGood()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File("f8ae9dc3-b990-4d08-bbba-c3b3fd6e18ae", ".png");
        filesModels.Add(fileModel);
        IEnumerable<File> files = filesModels;
        var result = TestFileProvider.GetAsync(files);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [Description("Test Get method with id good and extension wrong, i expected error")]
    [ExpectedException(typeof(ArgumentException))]
    public void GetFiles_ExtensionWrong()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File("f8ae9dc3-b990-4d08-bbba-c3b3fd6e18ae", ".png");
        filesModels.Add(fileModel);
        IEnumerable<File> files = filesModels;
        TestFileProvider.GetAsync(files);
    }

    [TestMethod]
    [Description("Test Get method with object null, i expected error")]
    [ExpectedException(typeof(ArgumentException))]
    public void GetFiles_ObjectNull()
    {
        List<File> filesModels = new List<File>();
        IEnumerable<File> files = filesModels;
        TestFileProvider.GetAsync(files);
    }
}
