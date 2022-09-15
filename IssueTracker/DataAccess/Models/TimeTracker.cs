namespace DataAccess.Models;
public class TimeTracker
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Worked { get; set; }
    public TimeSpan Billable { get; set; }
    public int IssueId { get; set; }
    public Guid UserId { get; set; }
}
