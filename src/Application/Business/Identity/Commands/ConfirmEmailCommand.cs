namespace Finisher.Application.Business.Identity.Commands;

public sealed record ConfirmEmailCommand(
    string Token
) : BaseNotIdCommand;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage(_ => Messages.NotNull);
    }
}
