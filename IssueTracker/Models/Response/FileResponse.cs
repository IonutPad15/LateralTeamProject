
namespace Models.Response;
public class FileResponse
{
    public string FileId { get; set; } = string.Empty;
    public string Extension { get; set; } = String.Empty;
    public int? FileIssueId { get; set; }
    public int? FileCommentId { get; set; }
}
