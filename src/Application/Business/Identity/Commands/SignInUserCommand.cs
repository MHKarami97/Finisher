using Finisher.Shared.Consts.Identity;

namespace Finisher.Application.Business.Identity.Commands;

public sealed record SignInUserCommand(
    string Phone
) : BaseNotIdCommand;

public class LoginUserCommandValidator : AbstractValidator<SignInUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MinimumLength(IdentityConsts.MinPhoneLength)
            .MaximumLength(IdentityConsts.MaxPhoneLength)
            .Matches(IdentityConsts.PhoneRegex)
            .WithMessage(_ => Messages.NotValidValue);
    }
}
