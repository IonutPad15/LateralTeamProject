
using Models.Response;

namespace Models.Request
{
    public class ParticipantRequest
    {
        public Guid UserId { get; set; }
        public int ProjectId { get; set; }
        public RolesInfo RoleId { get; set; }
    }
}
