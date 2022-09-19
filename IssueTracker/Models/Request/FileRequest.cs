using Microsoft.AspNetCore.Http;

namespace Models.Request;
public class FileRequest
{
    public FileRequest(IFormFile formFile, int issueId)
    {
        FormFile = formFile;
        IssueId = issueId;
    }

    public IFormFile FormFile { get; set; }
    public int IssueId { get; set; }
    public int? CommentId { get; set; }
}
