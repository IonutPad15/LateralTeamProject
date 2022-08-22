using FluentValidation;
using Models.Request;

namespace Validation
{
    public class ProjectValidation: AbstractValidator<ProjectRequest>
    {
        public ProjectValidation()
        {
            RuleFor(x => x).NotNull().WithMessage("It should not be null");
            RuleFor(x => x.Title).NotEmpty().
                MaximumLength(50).
                WithMessage("Title can't be empty or longer than 50");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Don't add empty descriptions");
        }
        public static bool IsValid(ProjectRequest project)
        {
            if(project == null) return false;
            if(project.Title == String.Empty || project.Title!.Length > 50) return false;
            if(project.Description == String.Empty) return false;
            return true;
        }
    }
}
