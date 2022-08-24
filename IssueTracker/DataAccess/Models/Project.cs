#pragma warning disable IDE0073 // The file header is missing or not located at the top of the file

namespace DataAccess.Models;
#pragma warning restore IDE0073 // The file header is missing or not located at the top of the file

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<Participant> Participants { get; set; } = Enumerable.Empty<Participant>();
    public IEnumerable<Issue> Issues { get; set; } = Enumerable.Empty<Issue>();
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
