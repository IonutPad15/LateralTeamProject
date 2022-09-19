namespace DataAccess.Models;
public class History
{
    public History(HistoryType historyType, int projectId, string author, DateTime updated)
    {
        Type = historyType;
        ProjectId = projectId;
        Author = author;
        Updated = updated;
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public History()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }
    public int Id { get; set; }
    public HistoryType Type { get; set; }
    public int ProjectId { get; set; }
    public string Author { get; set; }
    public int? IssueId { get; set; }
    public ReferenceType? ReferenceType { get; set; }
    public int? ReferenceId { get; set; }
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

