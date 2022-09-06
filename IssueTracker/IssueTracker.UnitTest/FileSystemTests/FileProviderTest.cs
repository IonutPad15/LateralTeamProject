using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest;
[TestClass]
public class FileProviderTest : BaseClass
{
    [TestMethod]
    [Description("Test Get method with id wrong and extension good, i expected exception")]

    public async Task GetFiles_IdWrongAsync()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File
        {
            Id = Guid.NewGuid().ToString(),
            Extension = ".png"
        };
        filesModels.Add(fileModel);

        IEnumerable<File> files = filesModels;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => FileProviderData.GetAsync(files));
    }

    [TestMethod]
    [Description("Test Get method with id good and extension good, i expected result")]
    public void GetFiles_IdGood()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File
        {
            Id = "f8ae9dc3-b990-4d08-bbba-c3b3fd6e18ae",
            Extension = ".png"
        };
        filesModels.Add(fileModel);
        IEnumerable<File> files = filesModels;

        var result = FileProviderData.GetAsync(files);

        Assert.IsNotNull(result);
    }

    [TestMethod]
    [Description("Test Get method with id good and extension wrong, i expected error")]
    public async Task GetFiles_ExtensionWrongAsync()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File
        {
            Id = "f8ae9dc3-b990-4d08-bbba-c3b3fd6e18ae",
            Extension = ".sasd"
        };
        filesModels.Add(fileModel);
        IEnumerable<File> files = filesModels;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => FileProviderData.GetAsync(files));
    }

    [TestMethod]
    [Description("Test Get method with object null, i expected error")]
    public async Task GetFiles_ObjectNullAsync()
    {
        List<File> filesModels = new List<File>();

        IEnumerable<File> files = filesModels;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => FileProviderData.GetAsync(files));
    }

    [TestMethod]
    [Description("Test Delete method with object null, i expected error")]
    public async Task DeleteFile_ObjectNullAsync()
    {
        File file = new();

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => FileProviderData.DeleteAsync(file));
    }

    [TestMethod]
    [Description("Test Delete method with id wrong and group good, i expected error")]
    public async Task DeleteFile_IdWrongAsync()
    {
        File file = new();
        file.Id = Guid.NewGuid().ToString();
        file.Extension = "png";

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => FileProviderData.DeleteAsync(file));
    }

    [TestMethod]
    [Description("Test Delete method with id good and group wrong , i expected error")]
    public async Task DeleteFile_GroupWrongAsync()
    {
        File file = new();
        file.Id = Guid.NewGuid().ToString();
        file.Extension = "c2be13af-9348-4c4c-b3ce-a138b7697e37";

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => FileProviderData.DeleteAsync(file));
    }

    [TestMethod]
    [Description("Test Upload method with null object , i expected error")]
    public async Task UploadFile_ObjectNullAsync()
    {
        File file = new();

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => FileProviderData.UploadAsync(file));
    }
}
