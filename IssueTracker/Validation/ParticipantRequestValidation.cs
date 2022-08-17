

using Models.Request;

namespace Validation
{
    public class ParticipantRequestValidation
    {
        public static bool IsValid(ParticipantRequest participant)
        {
            if(participant == null) return false;
            if(participant.ProjectId<=0) return false;
            if(participant.RoleId<=0) return false;
            return true;
        }
    }
}
