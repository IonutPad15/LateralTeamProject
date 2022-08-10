namespace DataAccess.Models
{
    public class Project
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Project()
        {
            Participants = new List<Participant>();
            Issues = new List<Issue>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        public List<Participant> Participants { get; set; } 
        public List<Issue> Issues { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
