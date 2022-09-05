using FluentValidation;
using IssueTracker.FileSystem.Models;

namespace Validation;
public class FileModelValidation : AbstractValidator<FileModel>
{
    public FileModelValidation(FileModel file)
    {
        RuleFor(f => f).NotNull().WithMessage("You don't have a file\\s!");
        RuleFor(f => f.Id).NotEmpty().WithMessage("Id file is empty!");
        RuleFor(f => f.Extension).NotEmpty().WithMessage("Extension file is empty!");
    }
}
