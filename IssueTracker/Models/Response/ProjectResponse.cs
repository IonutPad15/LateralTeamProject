namespace Models.Response
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public IEnumerable<ParticipantResponse>? Participants { get; set; } = Enumerable.Empty<ParticipantResponse>();
        public IEnumerable<IssueResponse>? Issues { get; set; } = Enumerable.Empty<IssueResponse>();
    }
}
