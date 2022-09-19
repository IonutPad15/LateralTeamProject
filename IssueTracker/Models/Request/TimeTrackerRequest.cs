namespace Models.Request;
public class TimeTrackerRequest
{
    public TimeTrackerRequest(string name, DateTime date, TimeSpan worked, TimeSpan billable, int issueId, Guid userId)
    {
        Name = name;
        Date = date;
        Worked = worked;
        Billable = billable;
        IssueId = issueId;
        UserId = userId;
    }

    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Worked { get; set; }
    public TimeSpan Billable { get; set; }
    public int IssueId { get; set; }
    public Guid UserId { get; set; }
}
