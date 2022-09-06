namespace Models.Response;
public class FileResponse
{
    public string FileId { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public int? IssueId { get; set; }
    public int? CommentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public double SizeKb { get; set; }
    public string Link { get; set; } = string.Empty;
}
