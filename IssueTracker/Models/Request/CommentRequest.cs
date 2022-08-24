
namespace Models.Request;

public class CommentRequest
{
    public CommentRequest(string body)
    {
        Body = body;
    }
    public int? IssueId { get; set; }
    public int? CommentId { get; set; }
    public string Body { get; set; }

}
