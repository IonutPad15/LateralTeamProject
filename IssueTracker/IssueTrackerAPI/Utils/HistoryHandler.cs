using DataAccess.Data.IData;
using DataAccess.Models;

namespace IssueTrackerAPI.Utils;

public class HistoryHandler
{
    private readonly IHistoryRepository _repository;
    public HistoryHandler(IHistoryRepository repository)
    {
        _repository = repository;
    }
    public async void CreatedComment(int projectId, int IssueId, int commentId, Guid userId, string newValue, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, userId, updated)
        {
            ReferenceType = ReferenceType.Comment,
            ReferenceId = commentId,
            Field = "Body",
            NewValue = newValue
        };
        await _repository.AddAsync(history);
    }
    public async void CreatedProject(int projectId, Guid userId, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, userId, updated);
        await _repository.AddAsync(history);
    }
    public async void CreatedIssue(int projectId, Guid userId, int issueId, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, userId, updated);
        history.IssueId = issueId;
        await _repository.AddAsync(history);
    }
    public async void CreatedParticipant(int projectId, int IssueId, int participantId, Guid userId, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, userId, updated)
        {
            ReferenceType = ReferenceType.Participant,
            ReferenceId = participantId
        };
        await _repository.AddAsync(history);
    }
}
