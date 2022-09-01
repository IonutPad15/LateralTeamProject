﻿using FluentValidation;
using Models.Request;

namespace Validation;
public class FileRequestValidation : AbstractValidator<FileRequest>
{
    public FileRequestValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.FormFile).NotNull().WithMessage("Null not allowed");
        RuleFor(x => x.IssueId).Null().When(x => x.CommentId != null).WithMessage("Only to an issue or comment");
        RuleFor(x => x.CommentId).Null().When(x => x.IssueId != null).WithMessage("Only to an issue or comment");
        RuleFor(x => x.IssueId).GreaterThan(0).When(x => x.CommentId == null).WithMessage("Id's start from 1");
        RuleFor(x => x.CommentId).GreaterThan(0).When(x => x.IssueId == null).WithMessage("Id's start from 1");
    }
}
