using Models.Request;
using FluentValidation;

namespace Validation;
public class CommentRequestValidation : AbstractValidator<CommentRequest>
{
    public CommentRequestValidation()
    {
        RuleFor(x => x.IssueId).NotNull().GreaterThan(0).
            WithMessage("It should be higher than 0");
        RuleFor(x => x.CommentId).GreaterThan(0).
            When(x => x.CommentId != null).
            WithMessage("It should be higher than 0");
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.Body).NotEmpty().Length(1, 450).WithMessage("Comment body too long");
    }
    public static bool IsValid(CommentRequest commentRequest)
    {
        if (commentRequest == null) return false;
        if (commentRequest.IssueId == null && commentRequest.CommentId == null) return false;
        if (commentRequest.IssueId <= 0) return false;
        if (commentRequest.CommentId <= 0) return false;
        if (string.IsNullOrEmpty(commentRequest.Body)) return false;
        if (commentRequest.Body.Length > 450) return false;
        return true;
    }
}
