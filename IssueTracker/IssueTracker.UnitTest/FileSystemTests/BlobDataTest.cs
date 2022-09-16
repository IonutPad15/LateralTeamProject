using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest.FileSystemTests;

[TestClass]
public class BlobDataTest : BaseClass
{
    [TestMethod]
    [Description("Upload Method with Content null, i expected error")]
    public async Task Upload_ContentNullAsync()
    {
        var file = new File
        {
            BlobName = $"{File.Id}.txt",
            Content = null
        };

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Upload Method with BlobName null, i expected error")]
    public async Task Upload_BlobNameNullAsync()
    {
        var file = new File
        {
            BlobName = null!,
            Content = File.Content
        };

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Upload Method with BlobName string empty, i expected error")]
    public async Task Upload_BlobNameStringEmptyAsync()
    {
        var file = new File
        {
            BlobName = String.Empty,
            Content = File.Content
        };

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Upload Method with good object, i expected result")]
    public async Task Upload_GoodObjectAsync()
    {
        var file = new File
        {
            BlobName = Guid.NewGuid().ToString(),
            Content = File.Content
        };

        await s_blobStorageProvider.UploadFileAsync(file);

        Assert.IsTrue(true);
    }

    [TestMethod]
    [Description("Get Method wiht null object, i expected error")]
    public async Task Get_ObjectNullAsync()
    {
        List<File> files = new();

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.GetFilesAsync(files));
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

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.GetFilesAsync(files));
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

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method wiht Extension null, i expected error")]
    public async Task Get_ExtensionNullAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = File.Id,
            Extension = null!
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method wiht Expresion string empty, i expected error")]
    public async Task Get_ExtensionStringEmptyAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = File.Id,
            Extension = String.Empty
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => s_blobStorageProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Get Method with good object, i expected result")]
    public async Task Get_GoodObjectAsync()
    {
        List<File> files = new();
        var file = new File
        {
            Id = File.Id,
            Extension = ".txt"
        };
        files.Add(file);

        var result = await s_blobStorageProvider.GetFilesAsync(files);

        Assert.IsNotNull(result);
    }
}
