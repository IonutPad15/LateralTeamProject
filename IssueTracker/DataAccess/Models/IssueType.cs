#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class IssueType
{
    public int Id { get; set; }
    public string Type { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
