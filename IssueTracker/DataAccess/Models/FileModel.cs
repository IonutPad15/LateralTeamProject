namespace DataAccess.Models;
public class FileModel
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string FileId { get; set; }
    public string GroupId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public int? FileIssueId { get; set; }
    public int? FileCommentId { get; set; }
}
