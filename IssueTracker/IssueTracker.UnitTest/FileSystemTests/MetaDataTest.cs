using File = IssueTracker.FileSystem.Models.File;
namespace IssueTracker.UnitTest;

[TestClass]
public class MetaDataTest : BaseClass
{
    [TestMethod]
    [Description("Given a valid entity, when CreateAsync is called" +
        "then it should add an entity in the azure table")]
    public async Task CreateAsync_Test()
    {
        //arrange
        var rowkey = Guid.NewGuid().ToString();
        var partitionkey = ".txt";
        string name = "TextForTest";
        string type = "TextDocument";
        double sizeKb = 12;
        var file = new File(rowkey, partitionkey, name, type, sizeKb);
        await TestMetaDataProvider.CreateAsync(file);
        //act
        List<File> files = new List<File>();
        files.Add(file);
        var result = TestMetaDataProvider.Get(files).FirstOrDefault();
        //assert
        Assert.AreEqual(result!.Name, name);
    }
    [TestMethod]
    [Description("Given an invalid entity (sizeKb = -12, partionKey=''), when CreateAsync is called" +
        "then it should throw an ArgumentException")]
    public async Task CreateAsyncBadRequest_Test()
    {
        //arrange
        var rowkey = Guid.NewGuid().ToString();
        var partitionkey = "";
        string name = "fisier";
        string type = "TextDocument";
        double sizeKb = -12;
        var file = new File(rowkey, partitionkey, name, type, sizeKb);

        await Assert.ThrowsExceptionAsync<ArgumentException>(() => TestMetaDataProvider.CreateAsync(file));
    }
    [TestMethod]
    [Description("Given a valid entity, when DeleteAsync is called" +
        "then it should delete the entity from the azure table")]
    public async Task DeleteAsync_Test()
    {
        //arrange
        var rowkey = Guid.NewGuid().ToString();
        var partitionkey = ".txt";
        string name = "fisier2";
        string type = "TextDocument";
        double sizeKb = 18;
        var file = new FileSystem.Models.File(rowkey, partitionkey, name, type, sizeKb);
        await TestMetaDataProvider.CreateAsync(file);
        //act
        var result = await TestMetaDataProvider.DeleteAsync(rowkey, partitionkey);
        //assert
        Assert.IsTrue(result);
    }
    [TestMethod]
    [Description("Given an invalid request (the rowkey does not exists in the azure table), when DeleteAsync is called" +
        "then the result must be false")]
    public async Task DeleteAsyncBadRequest_Test()
    {
        //arrange
        var rowkey = Guid.NewGuid().ToString();
        var partitionkey = ".txt";
        string name = "fisier2";
        string type = "TextDocument";
        double sizeKb = 18;
        var file = new File(rowkey, partitionkey, name, type, sizeKb);
        await TestMetaDataProvider.CreateAsync(file);
        //act
        var result = await TestMetaDataProvider.DeleteAsync(Guid.NewGuid().ToString(), partitionkey);
        //assert
        Assert.IsFalse(result);
    }
    [TestMethod]
    [Description("Given a valid request, when GetAsync is called" +
        "then it should get the entity from the azure table")]
    public async Task GetAsync_Test()
    {
        //arrange
        var rowkey = Guid.NewGuid().ToString();
        var partitionkey = ".txt";
        string name = "fisier4";
        string type = "TextDocument";
        double sizeKb = 12;
        var file = new FileSystem.Models.File(rowkey, partitionkey, name, type, sizeKb);
        await TestMetaDataProvider.CreateAsync(file);
        //act
        List<File> files = new List<File>();
        files.Add(file);
        var result = TestMetaDataProvider.Get(files).FirstOrDefault();
        //assert
        Assert.IsNotNull(result);
    }
    [TestMethod]
    [Description("Given an invalid request (the rowkey does not exists in the azure table), when GetAsync is called" +
        "then it should return a null object")]
    public void GetAsyncBadRequest_Test()
    {
        var file = new File(Guid.NewGuid().ToString(), ".png");
        List<File> files = new List<File>();
        files.Add(file);
        var result = TestMetaDataProvider.Get(files).FirstOrDefault();
        //assert
        Assert.IsNull(result);
    }
}
