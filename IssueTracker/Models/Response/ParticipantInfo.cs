namespace Models.Response
{
    public class ParticipantInfo
    {
        public int Id { get; set; }
        public UserInfo? User { get; set; }
        public IssueInfo? Issue { get; set; }
        public ProjectInfo? Project { get; set; }
    }
}
