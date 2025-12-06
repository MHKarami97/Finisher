using Finisher.Shared.Consts.Logger;

namespace Finisher.Application.Business.Logger.Commands;

public sealed record LoggerCreateCommand(
    string Message,
    DateTime Timestamp,
    string Path,
    string UserAgent
) : BaseNotIdCommand;

public class LoggerCreateCommandValidator : AbstractValidator<LoggerCreateCommand>
{
    public LoggerCreateCommandValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .MinimumLength(LoggerConsts.MinDescriptionLength)
            .MaximumLength(LoggerConsts.MaxDescriptionLength)
            .WithMessage(_ => Messages.ShortLengthBetween.FormatWith(LoggerConsts.MinDescriptionLength, LoggerConsts.MaxDescriptionLength));

        RuleFor(x => x.Timestamp)
            .NotEmpty()
            .WithMessage("Timestamp cannot be in the future.");

        RuleFor(x => x.Path)
            .NotEmpty()
            .MinimumLength(LoggerConsts.MinPathLength)
            .MaximumLength(LoggerConsts.MaxPathLength)
            .WithMessage(_ => Messages.ShortLengthBetween.FormatWith(LoggerConsts.MinPathLength, LoggerConsts.MaxPathLength));

        RuleFor(x => x.UserAgent)
            .NotEmpty()
            .MinimumLength(LoggerConsts.MinAgentLength)
            .MaximumLength(LoggerConsts.MaxAgentLength)
            .WithMessage(_ => Messages.ShortLengthBetween.FormatWith(LoggerConsts.MinAgentLength, LoggerConsts.MaxAgentLength));
    }
}
