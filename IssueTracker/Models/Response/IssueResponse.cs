namespace Models.Response;
public class IssueResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public IssueTypeResponse IssueType { get; set; } = new();
    public PriorityResponse Priority { get; set; } = new();
    public StatusResponse Status { get; set; } = new();
    public RoleResponse Role { get; set; } = null!;
    public ProjectResponse Project { get; set; } = new();
    public IEnumerable<FileResponse> Attachments { get; set; } = Enumerable.Empty<FileResponse>();
    public IEnumerable<HistoryResponse> Histories { get; set; } = Enumerable.Empty<HistoryResponse>();
}

