namespace Models.Request;
public class TimeTrackerRequest
{
    public TimeTrackerRequest(string name, DateTime date, int worked, int billable, int remaining, int issueId, Guid userId)
    {
        Name = name;
        Date = date;
        Worked = worked;
        Billable = billable;
        Remaining = remaining;
        IssueId = issueId;
        UserId = userId;
    }

    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public int Worked { get; set; }
    public int Billable { get; set; }
    public int Remaining { get; set; }
    public int IssueId { get; set; }
    public Guid UserId { get; set; }
}
