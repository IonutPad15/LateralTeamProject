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
    public async void CreatedParticipant(int projectId, int IssueId, int participantId, Guid userId, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, userId, updated)
        {
            ReferenceType = ReferenceType.Participant,
            ReferenceId = participantId
        };
        await _repository.AddAsync(history);
    }
    public async void UpdatedProject(int projectId, Guid userId, DateTime updated, string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, userId, updated)
        {
            Field = field,
            OldValue = oldValue,
            NewValue = newValue
        };
        await _repository.AddAsync(history);
    }
    public async void UpdatedIssue(int projectId, Guid userId, int issueId, DateTime updated,
        string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, userId, updated)
        {
            IssueId = issueId,
            Field = field,
            OldValue = oldValue,
            NewValue = newValue
        };
        await _repository.AddAsync(history);
    }
    public void UpdatedComment(int projectId, Guid userId, int issueId, DateTime updated,
        int commentId, string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, userId, updated)
        {
            IssueId = issueId,
            Field = field,
            ReferenceId = commentId,
            ReferenceType = ReferenceType.Comment,
            OldValue = oldValue,
            NewValue = newValue
        };
        _repository.AddAsync(history);
    }
    public void UpdatedParticipant(int projectId, Guid userId, int issueId, DateTime updated,
        int participantId, string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, userId, updated)
        {
            IssueId = issueId,
            Field = field,
            ReferenceId = participantId,
            ReferenceType = ReferenceType.Participant,
            OldValue = oldValue,
            NewValue = newValue
        };
        _repository.AddAsync(history);
    }
    public void DeletedProject(int projectId, Guid userId, DateTime updated)
    {
        History history = new History(HistoryType.Deleted, projectId, userId, updated);
        _repository.AddAsync(history);
    }
    public void DeletedIssue(int projectId, Guid userId, int issueId, DateTime updated)
    {
        History history = new History(HistoryType.Deleted, projectId, userId, updated);
        history.IssueId = issueId;
        _repository.AddAsync(history);
    }
    public void DeletedComment(int projectId, Guid userId, int issueId, DateTime updated, int commentId, string oldValue)
    {
        History history = new History(HistoryType.Deleted, projectId, userId, updated)
        {
            IssueId = issueId,
            ReferenceId = commentId,
            ReferenceType = ReferenceType.Comment,
            OldValue = oldValue,
        };
        _repository.AddAsync(history);
    }
    public void DeletedParticipant(int projectId, Guid userId, int issueId, DateTime updated,
        int participantId, string field, string oldValue)
    {
        History history = new History(HistoryType.Deleted, projectId, userId, updated)
        {
            IssueId = issueId,
            Field = field,
            ReferenceId = participantId,
            ReferenceType = ReferenceType.Participant,
            OldValue = oldValue,
        };
        _repository.AddAsync(history);
    }
}
