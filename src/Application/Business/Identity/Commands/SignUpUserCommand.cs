using Finisher.Shared.Consts.Identity;

namespace Finisher.Application.Business.Identity.Commands;

public sealed record SignUpUserCommand(
    string Phone
) : BaseNotIdCommand;

public class RegisterUserCommandValidator : AbstractValidator<SignUpUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(IdentityConsts.MaxPhoneLength)
            .Matches(IdentityConsts.PhoneRegex)
            .WithMessage(_ => Messages.NotValidValue);
    }
}
