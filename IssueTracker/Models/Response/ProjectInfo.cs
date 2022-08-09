namespace Models.Info
{
    public class ProjectInfo
    {
        public ProjectInfo()
        {
            Participants = new();
            Issues = new();
        }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public List<ParticipantInfo>? Participants { get; set; }
        public List<IssueInfo>? Issues { get; set; }
    }
}
