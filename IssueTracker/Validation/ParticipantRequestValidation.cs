using FluentValidation;
using Models.Request;

namespace Validation
{
    public class ParticipantRequestValidation: AbstractValidator<ParticipantRequest>
    {
        public ParticipantRequestValidation()
        {
            RuleFor(x => x).NotNull().WithMessage("It should not be null");
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Ids must be greater than 0");
            RuleFor(x => (int)x.RoleId).InclusiveBetween(1,4).WithMessage("Ids must be greater than 0");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("give a correct user id");
        }
        public static bool IsValid(ParticipantRequest participant)
        {
            if(participant == null) return false;
            if(participant.ProjectId<=0) return false;
            if(participant.RoleId<=0) return false;
            return true;
        }
    }
}
