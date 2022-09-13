namespace DataAccess.Models;
public class History
{
    public int Id { get; set; }
    public HistoryType Type { get; set; }
    public int ProjectId { get; set; }
    public Guid UserId { get; set; }
    public int? IssueId { get; set; }
    public ReferenceType? ReferenceType { get; set; }
    public int? ReferenceId { get; set; }
    public string? Field { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime Updated { get; set; }
    public History(HistoryType historyType, int projectId, Guid userId, DateTime updated)
    {
        Type = historyType;
        ProjectId = projectId;
        UserId = userId;
        Updated = updated;
    }
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

