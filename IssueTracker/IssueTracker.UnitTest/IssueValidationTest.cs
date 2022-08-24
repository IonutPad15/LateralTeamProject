using Models.Request;
using Validation;

namespace IssueTracker.UnitTest;

[TestClass]
public class IssueValidationTest
{
    [TestMethod]
    public void IsValid_Null_ReturnFalse()
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        IssueRequest issue = null;
        var result = IssueValidation.IsValid(issue!);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_TitleNull_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, null, "hgjhbhj", 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_TitleStringEmpty_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "", "hgjhbhj", 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_TitleMore50Length_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "hgjhbhj", 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_DescriptionNull_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", null, 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_DescriptionEmpty_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", "", 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_Id0_ReturnTrue()
    {
        IssueRequest issue = new IssueRequest(0, 1, "asdsadasd", "dsadsa", 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsTrue(result);
    }
    [TestMethod]
    public void IsValid_ProjectId0_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 0, "asdsadasd", "dsadsa", 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_IssueTypeId0_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", "dsadsa", 0, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_PriorityId0_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", "dsadsa", 1, Guid.NewGuid(), 0, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_StatusId0_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", "dsadsa", 1, Guid.NewGuid(), 1, 0, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_RoleId0_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", "dsadsa", 1, Guid.NewGuid(), 1, 1, 0);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_GuidEmpty_ReturnFalse()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", "dsadsa", 1, Guid.Empty, 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsFalse(result);
    }
    [TestMethod]
    public void IsValid_Issue_ReturnTrue()
    {
        IssueRequest issue = new IssueRequest(1, 1, "asdsadasd", "dsadsa", 1, Guid.NewGuid(), 1, 1, 1);

        var result = IssueValidation.IsValid(issue);

        Assert.IsTrue(result);
    }
}

