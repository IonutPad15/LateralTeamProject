using FluentValidation;
using Models.Request;
using Models.Response;
using TimeTracker = DataAccess.Models.TimeTracker;

namespace Validation;
public class TimeTrackerValidator : AbstractValidator<TimeTracker>
{
    public TimeTrackerValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.Name).NotEmpty().
            NotNull().
            Length(1, 20).
            WithMessage("Title must not be empty or longer than 20 characters");
        RuleFor(x => x.Description).NotEmpty().
            NotNull().WithMessage("Issue body must not be empty or null");
        RuleFor(x => x.IssueId).GreaterThan(0).WithMessage("ids are higher than 0!");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("ids are invalid!");
    }
    public static bool IsValid(TimeTrackerRequest entity)
    {
        if (entity == null)
            return false;
        if (string.IsNullOrEmpty(entity.Name))
            return false;
        if (entity.IssueId <= 0)
            return false;
        if (entity.UserId == Guid.Empty)
            return false;
        if (entity.Worked == TimeSpan.Zero || entity.Billable == TimeSpan.Zero)
            return false;
        return true;
    }
    public static bool IsValid(TimeTracker entity)
    {
        if (entity == null)
            return false;
        if (string.IsNullOrEmpty(entity.Name))
            return false;
        if (entity.IssueId <= 0)
            return false;
        if (entity.UserId == Guid.Empty)
            return false;
        if (entity.Worked == TimeSpan.Zero || entity.Billable == TimeSpan.Zero)
            return false;
        return true;
    }
    public static bool IsValid(TimeTrackerResponse entity)
    {
        if (entity == null)
            return false;
        if (entity.Id <= 0)
            return false;
        if (string.IsNullOrEmpty(entity.Name))
            return false;
        if (entity.IssueId <= 0)
            return false;
        if (entity.UserId == Guid.Empty)
            return false;
        if (entity.Worked == TimeSpan.Zero || entity.Billable == TimeSpan.Zero)
            return false;
        return true;
    }
}
