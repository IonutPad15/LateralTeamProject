namespace DataAccess.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Participant
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ProjectId { get; set; }
    public RolesType RoleId { get; set; }
    public bool IsDeleted { get; set; }
    public User? User { get; set; }
    public Project? Project { get; set; }
    public Role? Role { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
