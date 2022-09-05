using IssueTracker.FileSystem.Models;

namespace IssueTracker.UnitTest.FileSystem;
[TestClass]
public class FileProviderTest : BaseClass
{
    [TestMethod]
    [Description("Test Get method with id wrong and extension good, i expected exception")]
    [ExpectedException(typeof(ArgumentException))]
    public void GetFiles_IdWrong()
    {
        List<FileModel> filesModels = new List<FileModel>();
        var fileModel = new FileModel
        {
            Id = Guid.NewGuid().ToString(),
            Extension = ".png"
        };
        filesModels.Add(fileModel);
        IEnumerable<FileModel> files = filesModels;
        FileProviderData.GetAsync(files);
    }

    [TestMethod]
    [Description("Test Get method with id good and extension good, i expected result")]
    public void GetFiles_IdGood()
    {
        List<FileModel> filesModels = new List<FileModel>();
        var fileModel = new FileModel
        {
            Id = "f8ae9dc3-b990-4d08-bbba-c3b3fd6e18ae",
            Extension = ".png"
        };
        filesModels.Add(fileModel);
        IEnumerable<FileModel> files = filesModels;
        var result = FileProviderData.GetAsync(files);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [Description("Test Get method with id good and extension wrong, i expected error")]
    [ExpectedException(typeof(ArgumentException))]
    public void GetFiles_ExtensionWrong()
    {
        List<FileModel> filesModels = new List<FileModel>();
        var fileModel = new FileModel
        {
            Id = "f8ae9dc3-b990-4d08-bbba-c3b3fd6e18ae",
            Extension = ".sasd"
        };
        filesModels.Add(fileModel);
        IEnumerable<FileModel> files = filesModels;
        FileProviderData.GetAsync(files);
    }

    [TestMethod]
    [Description("Test Get method with object null, i expected error")]
    [ExpectedException(typeof(ArgumentException))]
    public void GetFiles_ObjectNull()
    {
        List<FileModel> filesModels = new List<FileModel>();
        IEnumerable<FileModel> files = filesModels;
        FileProviderData.GetAsync(files);
    }
}
