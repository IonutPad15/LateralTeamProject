using IssueTracker.FileSystem;
using File = IssueTracker.FileSystem.Models.File;

namespace IssueTracker.UnitTest;

[TestClass]
public class FileProviderTest : BaseClass
{
    [TestMethod]
    [Description("Given a invalid request Id is empty, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncWrongId_Test()
    {
        var file = File;
        file.Id = String.Empty;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a invalid request Id is null, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncNullId_Test()
    {
        var file = File;
        file.Id = null!;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a invalid request Extension is null, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncNullExtension_Test()
    {
        var file = File;
        file.Extension = null!;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a invalid request Extension is empty, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncEmptyExtension_Test()
    {
        var file = File;
        file.Extension = String.Empty;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a invalid request Content is null, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncNullContent_Test()
    {
        var file = File;
        file.Content = null;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a invalid request Size is negative value, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncNegativeSize_Test()
    {
        var file = File;
        file.SizeKb = -21;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a invalid request BlobName is null, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncNullBlobName_Test()
    {
        var file = File;
        file.BlobName = null;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a invalid request BlobName is empty, when UploadAsync is called" +
        "then I'm waiting for an error")]
    public async Task UploadAsyncEmptyBlobName_Test()
    {
        var file = File;
        file.BlobName = String.Empty;

        await Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.UploadAsync(file));
    }

    [TestMethod]
    [Description("Given a valid request, when GetAsync is called" +
        "then I'm waiting for an result")]
    public void GetAsync_Test()
    {
        List<File> filesModels = new List<File>();
        var fileModel = new File
        {
            Id = File.Id,
            Extension = ".png"
        };
        filesModels.Add(fileModel);
        IEnumerable<File> files = filesModels;
        var result = TestFileProvider.GetAsync(files);
        Assert.IsNotNull(result);
    }

    [TestMethod]
    [Description("Given a invalid request object is null, when GetAsync is called" +
        "then I'm waiting for an error")]
    public void GetAsyncNullObject_Test()
    {
        List<File> filesModels = new List<File>();
        IEnumerable<File> files = filesModels;
        Assert.ThrowsExceptionAsync<FileSystemException>(() => TestFileProvider.GetAsync(files));
    }
}
