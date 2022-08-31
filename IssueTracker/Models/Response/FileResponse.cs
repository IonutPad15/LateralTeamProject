
namespace Models.Response;
public class FileResponse
{
    public string FileId { get; set; } = string.Empty;
    public string GroupId { get; set; } = String.Empty;
    public int? FileIssueId { get; set; }
    public int? FileCommentId { get; set; }
}
