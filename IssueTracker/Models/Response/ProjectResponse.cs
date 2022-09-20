namespace Models.Response;
public class ProjectResponse
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ProjectResponse()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }
    public ProjectResponse(int id, string title, string description, DateTime created, IEnumerable<ParticipantResponse> participants, IEnumerable<IssueResponse> issues, IEnumerable<HistoryResponse> history)
    {
        Id = id;
        Title = title;
        Description = description;
        Created = created;
        Participants = participants;
        Issues = issues;
        History = history;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public IEnumerable<ParticipantResponse> Participants { get; set; } = Enumerable.Empty<ParticipantResponse>();
    public IEnumerable<IssueResponse> Issues { get; set; } = Enumerable.Empty<IssueResponse>();
    public IEnumerable<HistoryResponse> History { get; set; } = Enumerable.Empty<HistoryResponse>();
}
