namespace Models.Response
{
    public class IssueResponse
    {
        public IssueResponse()
        {
            Title = String.Empty;
            Description = String.Empty;
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int IssueTypeId { get; set; }
        public Guid? UserAssignedId { get; set; }
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public IssueTypeResponse IssueType { get; set; } = new IssueTypeResponse();
        public PriorityResponse Priority { get; set; } = new PriorityResponse();
        public StatusResponse Status { get; set; } = new StatusResponse();
        public RoleResponse Role { get; set; } = new RoleResponse();
        public ProjectResponse Project { get; set; } = new ProjectResponse();
        public UserResponse User { get; set; } = new UserResponse();
    }
}
