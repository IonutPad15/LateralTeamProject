using Models.Request;
using FluentValidation;

namespace Validation;
public class CommentRequestValidation : AbstractValidator<CommentRequest>
{
    public CommentRequestValidation()
    {
        RuleFor(x => x.IssueId).GreaterThan(0).
            When(x => x.IssueId != null).
            WithMessage("It should be higher than 0");
        RuleFor(x => x.CommentId).GreaterThan(0).
            When(x => x.CommentId != null).
            WithMessage("It should be higher than 0");
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.IssueId).NotNull().
            When(x => x.CommentId == null).
            WithMessage("Can't have issueId and commentId null");
        RuleFor(x => x.CommentId).NotNull().
            When(x => x.IssueId == null).
            WithMessage("Can't have issueId and commentId null");
        RuleFor(x => x.Body).NotEmpty().Length(1, 450).WithMessage("Comment body too long");
        RuleFor(x => x.IssueId).Null().
            When(x => x.CommentId != null).
            WithMessage("Can't have both issueId and commentId");
        RuleFor(x => x.CommentId).Null().
            When(x => x.IssueId != null).
            WithMessage("Can't have both issueId and commentId");
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
