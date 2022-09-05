namespace Models.Response;
public class IssueResponse
{
    public IssueResponse(int id, string title, string description, DateTime created,
        DateTime updated, IssueTypeResponse issueType, PriorityResponse priority,
        StatusResponse status, RoleResponse role, ProjectResponse project, IEnumerable<FileResponse> metaDatas)
    {
        Id = id;
        Title = title;
        Description = description;
        Created = created;
        Updated = updated;
        IssueType = issueType;
        Priority = priority;
        Status = status;
        Role = role;
        Project = project;
        MetaDatas = metaDatas;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public IssueTypeResponse IssueType { get; set; }
    public PriorityResponse Priority { get; set; }
    public StatusResponse Status { get; set; }
    public RoleResponse Role { get; set; }
    public ProjectResponse Project { get; set; }
    public IEnumerable<FileResponse> MetaDatas { get; set; }
}

