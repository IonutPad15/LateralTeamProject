namespace Models.Response;
public class HistoryResponse
{
    public HistoryResponse(int id, HistoryType type, int projectId, string author, DateTime updated)
    {
        Id = id;
        Type = type;
        ProjectId = projectId;
        Author = author;
        Updated = updated;
    }

    public int Id { get; set; }
    public HistoryType Type { get; set; }
    public int ProjectId { get; set; }
    public string Author { get; set; }
    public int? IssueId { get; set; }
    public ReferenceType? ReferenceType { get; set; }
    public string? ReferenceId { get; set; }
    public string? Field { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime Updated { get; set; }
}
public enum HistoryType
{
    Created = 1,
    Modified = 2,
    Deleted = 3,
}
public enum ReferenceType
{
    Comment = 1,
    Participant = 2,
    File = 3
}
