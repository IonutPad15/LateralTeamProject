namespace Models.Response;
public class CommentResponse
{
    public CommentResponse(int id, Guid? userId, string author, string body,
        DateTime created, DateTime updated, List<CommentResponse> replies, IEnumerable<FileResponse> metaDatas)
    {
        Id = id;
        UserId = userId;
        Author = author;
        Body = body;
        Created = created;
        Updated = updated;
        Replies = replies;
        MetaDatas = metaDatas;
    }

    public int Id { get; set; }
    public Guid? UserId { get; set; }
    public int? IssueId { get; set; }
    public int? CommentId { get; set; }
    public string Author { get; set; }
    public string Body { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public List<CommentResponse> Replies { get; set; }
    public IEnumerable<FileResponse> MetaDatas { get; set; }

}
