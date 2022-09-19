using FluentValidation;
using Models.Request;

namespace Validation;
public class FileDeleteRequestValidation : AbstractValidator<FileDeleteRequest>
{
    public FileDeleteRequestValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.FileId).Must(BeGuid).WithMessage("It must be a guid");
        RuleFor(x => x.GroupId).NotNull().NotEmpty().WithMessage("It can't be null or empty");
    }
    private bool BeGuid(string fileId)
    {
        if (Guid.TryParse(fileId, out var id))
            return true;
        return false;
    }

}
