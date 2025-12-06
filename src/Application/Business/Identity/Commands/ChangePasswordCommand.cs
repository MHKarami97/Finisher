using Finisher.Shared.Consts.Identity;

namespace Finisher.Application.Business.Identity.Commands;

public sealed record ChangePasswordCommand(
    string? CurrentPassword,
    string NewPassword
) : BaseNotIdCommand;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .MinimumLength(IdentityConsts.MinPasswordLength)
            .MaximumLength(IdentityConsts.MaxPasswordLength)
            .WithMessage(_ => Messages.ShortLengthBetween.FormatWith(IdentityConsts.MinPasswordLength, IdentityConsts.MaxPasswordLength));

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(IdentityConsts.MinPasswordLength)
            .MaximumLength(IdentityConsts.MaxPasswordLength)
            .WithMessage(_ => Messages.ShortLengthBetween.FormatWith(IdentityConsts.MinPasswordLength, IdentityConsts.MaxPasswordLength));
    }
}
