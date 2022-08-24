using Models.Response;

namespace Models.Request;

public class ParticipantRequest
{
    public ParticipantRequest(Guid userId, int projectId, RoleType roleId)
    {
        UserId = userId;
        ProjectId = projectId;
        RoleId = roleId;
    }

    public Guid UserId { get; set; }
    public int ProjectId { get; set; }
    public RoleType RoleId { get; set; }
}
