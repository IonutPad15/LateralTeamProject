using Models.Request;

namespace Validation
{
    public class ParticipantUpdateRequestValidation
    {
        public static bool IsValid(ParticipantUpdateRequest participant)
        {
            if(participant == null) return false;
            if(participant.Id<=0) return false;
            if(participant.RoleId<=0) return false;
            return true;
        }
    }
}
