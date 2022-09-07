using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest.FileSystemTests;

[TestClass]
public class BlobStorageProviderTest : BaseClass
{
    [TestMethod]
    [Description("Upload Method with Content null, i expected error")]
    public async Task Upload_ContentNullAsync()
    {
        var file = new File
        {
            BlobName = $"{FileObject.Id}.txt",
            Content = null
        };

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Upload Method with BlobName null, i expected error")]
    public async Task Upload_BlobNameNullAsync()
    {
        var file = new File
        {
            BlobName = null!,
            Content = FileObject.Content
        };

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Upload Method with BlobName string empty, i expected error")]
    public async Task Upload_BlobNameStringEmptyAsync()
    {
        var file = new File
        {
            BlobName = String.Empty,
            Content = FileObject.Content
        };

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Get Method wiht null object, i expected error")]
    public async Task Get_ObjectNullAsync()
    {
        List<File> files = new();

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method wiht Id null, i expected error")]
    public async Task Get_IdNullAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = null!,
            Extension = ".txt"
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method wiht Id string empty, i expected error")]
    public async Task Get_IdStringEmptyAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = String.Empty,
            Extension = ".txt"
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method wiht Extension null, i expected error")]
    public async Task Get_ExtensionNullAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = FileObject.Id,
            Extension = null!
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method wiht Expresion string empty, i expected error")]
    public async Task Get_ExtensionStringEmptyAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = FileObject.Id,
            Extension = String.Empty
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobData.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method with good object, i expected result")]
    public async Task Get_GoodObjectAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = FileObject.Id,
            Extension = ".txt"
        };
        files.Add(file);

        var result = await s_blobData.GetFilesAsync(files);

        Assert.IsNotNull(result);
    }
}
