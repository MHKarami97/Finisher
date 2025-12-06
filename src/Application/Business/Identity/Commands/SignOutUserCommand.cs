namespace Finisher.Application.Business.Identity.Commands;

public sealed record SignOutUserCommand(
    string? Message
) : BaseNotIdCommand;
