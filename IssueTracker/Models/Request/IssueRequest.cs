namespace Models.Request;
public class IssueRequest
{
    public IssueRequest(int id, int projectId, string title, string description, int issueTypeId, Guid userAssignedId, int priorityId, int statusId, int roleId)
    {
        Id = id;
        ProjectId = projectId;
        Title = title;
        Description = description;
        IssueTypeId = issueTypeId;
        UserAssignedId = userAssignedId;
        PriorityId = priorityId;
        StatusId = statusId;
        RoleId = roleId;
    }

    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int IssueTypeId { get; set; }
    public Guid UserAssignedId { get; set; }
    public int PriorityId { get; set; }
    public int StatusId { get; set; }
    public int RoleId { get; set; }
}
