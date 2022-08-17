
using Models.Response;

namespace Models.Request
{
    public class ParticipantUpdateRequest
    {
        public ParticipantUpdateRequest(int id, int projectId, RoleType roleId)
        {
            Id = id;
            ProjectId = projectId;
            RoleId = roleId;
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public RoleType RoleId { get; set; }
    }
}
