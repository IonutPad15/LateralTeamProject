using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest;

[TestClass]
public class FileProviderTest : BaseClass
{
    [TestMethod]
    [Description("Testing Upload Method with Id Empty, i expected error!")]
    public async Task Upload_IdWrongAsync()
    {
        var file = FileObject;
        file.Id = String.Empty;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Testing Upload Method with Id null, i expected error!")]
    public async Task Upload_IdNullAsync()
    {
        var file = FileObject;
        file.Id = null!;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Testing Upload Method with Extension null, i expected error!")]
    public async Task Upload_ExtensionNullAsync()
    {
        var file = FileObject;
        file.Extension = null!;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Testing Upload Method with Extension empty, i expected error!")]
    public async Task Upload_ExtensionEmptyAsync()
    {
        var file = FileObject;
        file.Extension = String.Empty;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Testing Upload Method with Content null, i expected error!")]
    public async Task Upload_ContentNullAsync()
    {
        var file = FileObject;
        file.Content = null;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Testing Upload Method with Size negative, i expected error!")]
    public async Task Upload_SizeNegativeAsync()
    {
        var file = FileObject;
        file.SizeKb = -21;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Testing Upload Method with BlobName Null, i expected error!")]
    public async Task Upload_BlobNameNullAsync()
    {
        var file = FileObject;
        file.BlobName = null;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Testing Upload Method with BlobName empty, i expected error!")]
    public async Task Upload_BlobNameEmptyAsync()
    {
        var file = FileObject;
        file.BlobName = String.Empty;

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.UploadAsync(file));
    }

    [TestMethod]
    [Description("Test Get method with id good and extension good, i expected result")]
    public void GetFiles_IdGood()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File
        {
            Id = FileObject.Id,
            Extension = ".png"
        };
        filesModels.Add(fileModel);
        IEnumerable<File> files = filesModels;
        var result = s_fileProviderData.GetAsync(files);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [Description("Test Get method with object null, i expected error")]
    public void GetFiles_ObjectNull()
    {
        List<File> filesModels = new List<File>();
        IEnumerable<File> files = filesModels;
        Assert.ThrowsExceptionAsync<ArgumentException>(() => s_fileProviderData.GetAsync(files));
    }
}
