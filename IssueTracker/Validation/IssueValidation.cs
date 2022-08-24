using FluentValidation;
using Models.Request;

namespace Validation;
public class IssueValidation : AbstractValidator<IssueRequest>
{
    public IssueValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.Title).NotEmpty().
            NotNull().
            Length(1, 50).
            WithMessage("Title must not be empty or longer than 50 characters");
        RuleFor(x => x.Title).NotEmpty().
            NotNull().WithMessage("Issue body must not be empty or null");
        RuleFor(x => x.IssueTypeId).GreaterThan(0).WithMessage("ids are higher than 0!");
        RuleFor(x => x.PriorityId).GreaterThan(0).WithMessage("ids are higher than 0!");
        RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("ids are higher than 0!");
        RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("ids are higher than 0!");
        RuleFor(x => x.StatusId).GreaterThan(0).WithMessage("ids are higher than 0!");
    }
    public static bool IsValid(IssueRequest issue)
    {
        if (issue == null) return false;
        if (issue.Title == null || issue.Title == String.Empty || issue.Title.Length > 50) return false;
        if (issue.Description == null || issue.Description == String.Empty) return false;
        if (issue.UserAssignedId == Guid.Empty) return false;
        if (issue.ProjectId == 0
            || issue.IssueTypeId == 0
            || issue.StatusId == 0
            || issue.PriorityId == 0
            || issue.RoleId == 0) return false;
        return true;
    }
}
