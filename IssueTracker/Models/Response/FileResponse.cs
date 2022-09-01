
namespace Models.Response;
public class FileResponse
{
    public FileResponse(string fileId, string extension, int issueId, string name, string link)
    {
        FileId = fileId;
        Extension = extension;
        IssueId = issueId;
        Name = name;
        Link = link;
    }

    public string FileId { get; set; } = string.Empty;
    public string Extension { get; set; } = String.Empty;
    public int IssueId { get; set; }
    public int? CommentId { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
}
