using FluentValidation;
using Models.Request;

namespace Validation;
public class UserValidation : AbstractValidator<UserRequest>
{
    public UserValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.Email).NotNull().
            Length(1, 450).
            WithMessage("Email should be between 1 and 450 characters");
        RuleFor(x => x.UserName).NotNull().
            Length(3, 450).
            WithMessage("Username should be between 1 and 450 characters");
        RuleFor(x => x.Password).NotNull().NotEmpty().Must(BeLongerThan8).WithMessage("Password too short");
    }
    private bool BeLongerThan8(string? password)
    {
        return password?.Count() >= 8;
    }
    public static bool IsValid(UserRequest userRequest)
    {
        if (userRequest == null) return false;
        if (string.IsNullOrEmpty(userRequest.UserName) || string.IsNullOrEmpty(userRequest.Password)
            || string.IsNullOrEmpty(userRequest.Email)) return false;
        if (userRequest.UserName.Length > 50 || userRequest.Email.Length > 450)
            return false;
        return true;
    }
}
