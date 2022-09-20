using IssueTracker.FileSystem;
using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest.FileSystemTests;

[TestClass]
public class BlobProviderTest : BaseClass
{
    [TestMethod]
    [Description("Given an invalid entity (null content), when UploadAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task UploadAsyncContentNull_Test()
    {
        var file = new File
        {
            BlobName = $"{File.Id}.txt",
            Content = null
        };

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Given an invalid entity (null blobName), when UploadAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task UploadAsync_BlobNameNull()
    {
        var file = new File
        {
            BlobName = null!,
            Content = File.Content
        };

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Given an invalid entity (blobName is ''), when UploadAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task UploadAsyncBlobNameEmpty_Test()
    {
        var file = new File
        {
            BlobName = String.Empty,
            Content = File.Content
        };

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.UploadFileAsync(file));
    }

    [TestMethod]
    [Description("Given a valid entity, when UploadAsync is called" +
        "then it should return a value")]
    public async Task UploadAsyncGoodObject_Test()
    {
        var path = "TextForTest2.txt";
        FileStream fss = System.IO.File.Create(path);
        fss.Close();
        using (FileStream fs = System.IO.File.OpenRead(path))
        {
            string id = Guid.NewGuid().ToString();
            var file = new File(id, ".txt")
            {
                BlobName = id,
                Content = fs
            };
            await TestBlobProvider.UploadFileAsync(file);
            fs.Close();
            Assert.IsTrue(true);
        }
    }

    [TestMethod]
    [Description("Given an invalid entity (null content), when GetAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task GetAsyncNullObject_Test()
    {
        List<File> files = new();

        var result = await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Given an invalid entity (null id), when GetAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task GetAsyncNullId_Test()
    {
        List<File> files = new();
        var file = new File
        {
            Id = null!,
            Extension = ".txt"
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Given an invalid entity (id is ''), when GetAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task GetAsyncIdStringEmpty_Test()
    {
        List<File> files = new();
        var file = new File
        {
            Id = String.Empty,
            Extension = ".txt"
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Given an invalid entity (null extension), when GetAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task GetAsyncExtensionNull_Test()
    {
        List<File> files = new();
        var file = new File
        {
            Id = File.Id,
            Extension = null!
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Given an invalid entity (extension is ''), when GetAsync is called" +
        "then it should throw an FileSystemException")]
    public async Task GetAsyncExtensionStringEmpty_Test()
    {
        List<File> files = new();
        var file = new File
        {
            Id = File.Id,
            Extension = String.Empty
        };
        files.Add(file);

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestBlobProvider.GetFilesAsync(files));
    }

    [TestMethod]
    [Description("Given a valid entity, when GetAsync is called" +
        "then it should get an result")]
    public async Task Get_GoodObjectAsync()
    {
        List<File> files = new();
        var file = new File(Guid.NewGuid().ToString(), ".txt");
        files.Add(file);

        var result = await TestBlobProvider.GetFilesAsync(files);

        Assert.IsNotNull(result);
    }
}
