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
        public IssueTypeResponse IssueType { get; set; }
        public PriorityResponse Priority { get; set; }
        public StatusResponse Status { get; set; }
        public RoleResponse Role { get; set; }
        public ProjectResponse Project { get; set; }
        public UserResponse User { get; set; }
    }
}
