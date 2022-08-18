using FluentValidation;
using Models.Request;

namespace Validation
{
    public class ParticipantUpdateRequestValidation : AbstractValidator<ParticipantUpdateRequest>
    {
        public ParticipantUpdateRequestValidation()
        {
            RuleFor(x => x).NotNull().WithMessage("It should not be null");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Ids should be greater than 0!");
            RuleFor(x => (int)x.RoleId).GreaterThan(0).WithMessage("Ids should be greater than 0!");
        }
        public static bool IsValid(ParticipantUpdateRequest participant)
        {
            if(participant == null) return false;
            if(participant.Id<=0) return false;
            if(participant.RoleId<=0) return false;
            return true;
        }
    }
}
