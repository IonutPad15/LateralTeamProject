namespace Models.Response
{
    public class ParticipantResponse
    {
        public int Id { get; set; }
        public UserResponse? User { get; set; }
        public IssueResponse? Issue { get; set; }
        public ProjectResponse? Project { get; set; }
    }
}
