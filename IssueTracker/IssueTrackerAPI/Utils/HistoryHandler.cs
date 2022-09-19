using DataAccess.Repository;
using DataAccess.Models;
using System.Text;

namespace IssueTrackerAPI.Utils;

public class HistoryHandler
{
    private readonly IHistoryRepository _repository;
    public HistoryHandler(IHistoryRepository repository)
    {
        _repository = repository;
    }
    public async void CreatedProject(int projectId, string author, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, author, updated);
        await _repository.AddAsync(history);
    }
    public async void CreatedIssue(int projectId, string author, int issueId, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, author, updated)
        {
            IssueId = issueId
        };
        await _repository.AddAsync(history);
    }
    public async void CreatedComment(int projectId, int IssueId, int commentId, string author, string newValue, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, author, updated)
        {
            IssueId = IssueId,
            ReferenceType = ReferenceType.Comment,
            ReferenceId = commentId.ToString(),
            Field = "Body",
            NewValue = newValue
        };
        await _repository.AddAsync(history);
    }
    public async void CreatedParticipant(int projectId, int participantId, string field, string newValue, string author, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, author, updated)
        {
            ReferenceType = ReferenceType.Participant,
            ReferenceId = participantId.ToString(),
            Field = field,
            NewValue = newValue
        };
        await _repository.AddAsync(history);
    }
    public async void CreatedFile(int projectId, int issueId, string fileId, string author, DateTime updated)
    {
        History history = new History(HistoryType.Created, projectId, author, updated)
        {
            IssueId = issueId,
            ReferenceType = ReferenceType.File,
            ReferenceId = fileId,
        };
        await _repository.AddAsync(history);
    }
    public async void UpdatedProject(int projectId, string author, DateTime updated, string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, author, updated)
        {
            Field = field,
            OldValue = oldValue,
            NewValue = newValue
        };
        await _repository.AddAsync(history);
    }
    public async void UpdatedIssue(int projectId, string author, int issueId, DateTime updated,
        string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, author, updated)
        {
            IssueId = issueId,
            Field = field,
            OldValue = oldValue,
            NewValue = newValue
        };
        await _repository.AddAsync(history);
    }
    public void UpdatedComment(int projectId, string author, int issueId, DateTime updated,
        int commentId, string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, author, updated)
        {
            IssueId = issueId,
            Field = field,
            ReferenceId = commentId.ToString(),
            ReferenceType = ReferenceType.Comment,
            OldValue = oldValue,
            NewValue = newValue
        };
        _repository.AddAsync(history);
    }
    public void UpdatedParticipant(int projectId, string author, DateTime updated,
        int participantId, string field, string oldValue, string newValue)
    {
        History history = new History(HistoryType.Modified, projectId, author, updated)
        {
            Field = field,
            ReferenceId = participantId.ToString(),
            ReferenceType = ReferenceType.Participant,
            OldValue = oldValue,
            NewValue = newValue
        };
        _repository.AddAsync(history);
    }
    public void DeletedProject(int projectId, string author, DateTime updated)
    {
        History history = new History(HistoryType.Deleted, projectId, author, updated);
        _repository.AddAsync(history);
    }
    public void DeletedIssue(int projectId, string author, int issueId, DateTime updated)
    {
        History history = new History(HistoryType.Deleted, projectId, author, updated);
        history.IssueId = issueId;
        _repository.AddAsync(history);
    }
    public void DeletedComment(int projectId, string author, int issueId, DateTime updated, int commentId, string oldValue)
    {
        History history = new History(HistoryType.Deleted, projectId, author, updated)
        {
            IssueId = issueId,
            ReferenceId = commentId.ToString(),
            ReferenceType = ReferenceType.Comment,
            OldValue = oldValue,
        };
        _repository.AddAsync(history);
    }
    public void DeletedParticipant(int projectId, string author, DateTime updated,
        int participantId)
    {
        History history = new History(HistoryType.Deleted, projectId, author, updated)
        {
            ReferenceId = participantId.ToString(),
            ReferenceType = ReferenceType.Participant,
        };
        _repository.AddAsync(history);
    }
    public async void DeletedFile(int projectId, int issueId, string fileId, string oldValue, string author, DateTime updated)
    {
        History history = new History(HistoryType.Deleted, projectId, author, updated)
        {
            IssueId = issueId,
            ReferenceType = ReferenceType.File,
            ReferenceId = fileId,
            OldValue = oldValue
        };
        await _repository.AddAsync(history);
    }
    public void ProjectUpdatedValues(Project oldProject, Project newProject, out string field, out string oldValue, out string newValue)
    {
        StringBuilder sbField = new StringBuilder("");
        StringBuilder sbOldValues = new StringBuilder("");
        StringBuilder sbNewValues = new StringBuilder("");
        if (oldProject.Title != newProject.Title)
        {
            sbField.Append("Title; ");
            sbOldValues.Append($"{oldProject.Title}; ");
            sbNewValues.Append($"{newProject.Title}; ");
        }
        if (oldProject.Description != newProject.Description)
        {
            sbField.Append("Description; ");
            sbOldValues.Append($"{oldProject.Description}; ");
            sbNewValues.Append($"{newProject.Description}; ");
        }
        field = sbField.ToString();
        oldValue = sbOldValues.ToString();
        newValue = sbNewValues.ToString();
    }
    public void IssueUpdatedValues(Issue oldIssue, Issue newIssue, out string field, out string oldValue, out string newValue)
    {
        StringBuilder sbField = new StringBuilder("");
        StringBuilder sbOldValues = new StringBuilder("");
        StringBuilder sbNewValues = new StringBuilder("");
        if (oldIssue.Title != newIssue.Title)
        {
            sbField.Append("Title; ");
            sbOldValues.Append($"{oldIssue.Title}; ");
            sbNewValues.Append($"{newIssue.Title}; ");
        }
        if (oldIssue.Description != newIssue.Description)
        {
            sbField.Append("Description; ");
            sbOldValues.Append($"{oldIssue.Description}; ");
            sbNewValues.Append($"{newIssue.Description}; ");
        }
        if (oldIssue.IssueTypeId != newIssue.IssueTypeId)
        {
            sbField.Append("IssueType; ");
            sbOldValues.Append($"{oldIssue.IssueTypeId.ToString()}; ");
            sbNewValues.Append($"{newIssue.IssueTypeId.ToString()}; ");
        }
        if (oldIssue.StatusId != newIssue.StatusId)
        {
            sbField.Append("Status; ");
            sbOldValues.Append($"{oldIssue.StatusId.ToString()}; ");
            sbNewValues.Append($"{newIssue.StatusId.ToString()}; ");
        }
        if (oldIssue.UserAssignedId != newIssue.UserAssignedId)
        {
            sbField.Append("UserAssignedId; ");
            sbOldValues.Append($"{oldIssue.UserAssignedId}; ");
            sbNewValues.Append($"{newIssue.UserAssignedId}; ");
        }
        if (oldIssue.PriorityId != newIssue.PriorityId)
        {
            sbField.Append("Priority; ");
            sbOldValues.Append($"{oldIssue.PriorityId.ToString()}; ");
            sbNewValues.Append($"{newIssue.StatusId.ToString()}; ");
        }
        if (oldIssue.RoleId != newIssue.RoleId)
        {
            sbField.Append("Role; ");
            sbOldValues.Append($"{oldIssue.RoleId.ToString()}; ");
            sbNewValues.Append($"{newIssue.RoleId.ToString()}; ");
        }
        field = sbField.ToString();
        oldValue = sbOldValues.ToString();
        newValue = sbNewValues.ToString();
    }
}
