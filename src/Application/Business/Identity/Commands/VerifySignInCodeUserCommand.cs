using Finisher.Shared.Consts.Identity;

namespace Finisher.Application.Business.Identity.Commands;

public sealed record VerifySignInCodeUserCommand(
    string Phone,
    string Code
) : BaseNotIdCommand;

public class VerifySignInCodeUserCommandValidator : AbstractValidator<VerifySignInCodeUserCommand>
{
    public VerifySignInCodeUserCommandValidator()
    {
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MinimumLength(IdentityConsts.MinPhoneLength)
            .MaximumLength(IdentityConsts.MaxPhoneLength)
            .Matches(IdentityConsts.PhoneRegex)
            .WithMessage(_ => Messages.NotValidValue);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MinimumLength(IdentityConsts.MinVerifyCodeLength)
            .MaximumLength(IdentityConsts.MaxVerifyCodeLength)
            .WithMessage(_ => Messages.NotValidValue);
    }
}
