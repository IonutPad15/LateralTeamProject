namespace DataAccess.Models;
public class FileModel
{
    public FileModel(string fileId)
    {
        FileId = fileId;
    }
    public int Id { get; set; }
    public string FileId { get; set; }
    public int? IssueId { get; set; }
    public int? CommentId { get; set; }
    public bool IsDeleted { get; set; }
}
