namespace DataAccess.Models;

public class Comment
{
    public int Id { get; set; }
    public Guid? UserId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public int IssueId { get; set; }
    public int? CommentId { get; set; }

    public string Author { get; set; }
    public string Body { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool IsDeleted { get; set; }
    public User? User { get; set; }
    public IEnumerable<Comment> Replies { get; set; } = Enumerable.Empty<Comment>();
    public IEnumerable<Models.File> Attachements { get; set; } = Enumerable.Empty<Models.File>();
}
