namespace Models.Response
{
    public class ProjectResponse
    {
        public ProjectResponse()
        {
            Participants = new();
            Issues = new();
        }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public List<ParticipantResponse>? Participants { get; set; }
        public List<IssueResponse>? Issues { get; set; }
    }
}
