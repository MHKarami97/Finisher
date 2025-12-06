namespace Finisher.Shared.Exceptions;

public static class ExceptionInfo
{
    public static readonly SystemExceptionType Error500 = new(nameof(Messages.UnhandledException), Messages.UnhandledException, "contact_support");
    public static readonly SystemExceptionType Error401 = new(nameof(Messages.UnhandledException), Messages.UnhandledException, "contact_support");
    public static readonly SystemExceptionType Error403 = new(nameof(Messages.UnhandledException), Messages.UnhandledException, "contact_support");
    public static readonly SystemExceptionType Error404 = new(nameof(Messages.UnhandledException), Messages.UnhandledException, "contact_support");
}

public record SystemExceptionType(string Type, string Reason, string UserAction);
