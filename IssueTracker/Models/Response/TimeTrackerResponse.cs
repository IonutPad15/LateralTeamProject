namespace Models.Response;
public class TimeTrackerResponse
{
    public TimeTrackerResponse(int id, string name, DateTime date, TimeSpan worked, TimeSpan billable, int issueId, Guid userId)
    {
        Id = id;
        Name = name;
        Date = date;
        Worked = worked;
        Billable = billable;
        IssueId = issueId;
        UserId = userId;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Worked { get; set; }
    public TimeSpan Billable { get; set; }
    public int IssueId { get; set; }
    public Guid UserId { get; set; }
}
