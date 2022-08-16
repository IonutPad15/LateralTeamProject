namespace Models.Request
{
    public class IssueRequest
    {
        public IssueRequest()
        {
            Title = String.Empty;
            Description = String.Empty;
            UserAssignedId = Guid.Empty;
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
}
