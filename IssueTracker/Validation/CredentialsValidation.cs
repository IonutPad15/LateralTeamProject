using FluentValidation;
using Models.Request;

namespace Validation;
public class CredentialsValidation : AbstractValidator<Credentials>
{
    public CredentialsValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("It should not be null");
        RuleFor(x => x.NameEmail).NotNull().
            NotEmpty().
            Length(1, 50).WithMessage("username too long/short/null");
    }
    public static bool IsValid(Credentials credentials)
    {
        if (credentials == null) return false;
        if (string.IsNullOrEmpty(credentials.NameEmail) || string.IsNullOrEmpty(credentials.Password))
            return false;
        if (credentials.NameEmail.Length > 450) return false;
        return true;
    }
}
