namespace DataAccess.Models;

public class Issue
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public int IssueTypeId { get; set; }
    public Guid? UserAssignedId { get; set; }
    public int PriorityId { get; set; }
    public int StatusId { get; set; }
    public RolesType RoleId { get; set; }
    public bool IsDeleted { get; set; }
    public IssueType IssueType { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public Role Role { get; set; }
    public Project Project { get; set; }
    public User User { get; set; }
    //TODO: add List<FileModel> and read it from db with join

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
