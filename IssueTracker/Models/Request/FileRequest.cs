using Microsoft.AspNetCore.Http;

namespace Models.Request;
public class FileRequest
{
    public FileRequest(IFormFile formFile, int? issueId, int? commentId)
    {
        FormFile = formFile;
        IssueId = issueId;
        CommentId = commentId;
    }

    public IFormFile FormFile { get; set; }
    public int? IssueId { get; set; }
    public int? CommentId { get; set; }
}
