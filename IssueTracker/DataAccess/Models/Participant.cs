namespace DataAccess.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProjectId { get; set; }
        public int IssueId { get; set; }
    }
}
//TODO: object foreignKey User
